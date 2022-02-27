import React from 'react';

import { useTranslation } from 'react-i18next';
import { useDispatch, useSelector } from 'react-redux';
import { matchPath, useLocation } from 'react-router-dom';

import ChannelListContainer from '@/components/domain/chat/ChannelListContainer';
import CreateChannelModal from '@/components/domain/chat/CreateChannelModal';
import DirectMessage from '@/components/domain/chat/DirectMessage';
import { addChatChannel } from '@/store/chat/chat.actions';
import { channelsSelector } from '@/store/chat/chat.selectors';
import feedbackUtils from '@/utils/feedback.utils';

const Explorer = ({ ...props }) => {
  const [modalIsOpen, setModalIsOpen] = React.useState(false);
  const { pathname } = useLocation();
  const { t } = useTranslation();
  const channels = useSelector(channelsSelector);
  const dispatch = useDispatch();

  const {
    params: { channelId },
  } = matchPath('/channels/:channelId', pathname) || { params: {} };

  const onCreate = (channel) => {
    feedbackUtils.positive(t('channels.created.message'));
    setModalIsOpen(false);
    dispatch(addChatChannel(channel, true));
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
