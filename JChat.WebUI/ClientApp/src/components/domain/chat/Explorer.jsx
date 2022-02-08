import React from 'react';

import { useTranslation } from 'react-i18next';
import { useSelector } from 'react-redux';
import { matchPath, useLocation } from 'react-router-dom';

import ChannelListContainer from '@/components/domain/chat/ChannelListContainer';
import CreateChannelModal from '@/components/domain/chat/CreateChannelModal';
import DirectMessage from '@/components/domain/chat/DirectMessage';
import { channelsSelector } from '@/store/boot/boot.selectors';
import feedbackUtils from '@/utils/feedback.utils';

const Explorer = ({ ...props }) => {
  const channels = useSelector(channelsSelector);
  const [modalIsOpen, setModalIsOpen] = React.useState(false);
  const { pathname } = useLocation();
  const { t } = useTranslation();
  const {
    params: { channelId },
  } = matchPath('/channels/:channelId', pathname) || { params: {} };
  console.log(channels);

  const onCreate = (channel) => {
    feedbackUtils.positive(t('channels.created.message'));
    setModalIsOpen(false);
    // setChannels([channel, ...channels]);
  };

  return (
    <>
      <CreateChannelModal
        onCreate={onCreate}
        isOpen={modalIsOpen}
        setIsOpen={setModalIsOpen}
      />
      <div {...props}>
        <ChannelListContainer
          title={t('explorer.channels')}
          onAdd={() => {
            setModalIsOpen(true);
          }}
        >
          {channels.map(({ id, name }) => (
            <DirectMessage
              key={id}
              id={id}
              isActive={channelId === id}
              name={name}
            />
          ))}
        </ChannelListContainer>
        {/*<ChannelListContainer*/}
        {/*  title={t('explorer.direct')}*/}
        {/*></ChannelListContainer>*/}
      </div>
    </>
  );
};

export default Explorer;
