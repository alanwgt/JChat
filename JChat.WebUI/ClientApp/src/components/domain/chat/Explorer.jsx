import React from 'react';

import { useStyletron } from 'baseui';
import { useTranslation } from 'react-i18next';
import { matchPath, useLocation } from 'react-router-dom';

import { Channels } from '@/api';
import Downloadable from '@/components/data-display/Downloadable';
import ChannelListContainer from '@/components/domain/chat/ChannelListContainer';
import CreateChannelModal from '@/components/domain/chat/CreateChannelModal';
import DirectMessage from '@/components/domain/chat/DirectMessage';
import feedbackUtils from '@/utils/feedback.utils';

const Explorer = ({ ...props }) => {
  const [modalIsOpen, setModalIsOpen] = React.useState(false);
  const { pathname } = useLocation();
  const [css] = useStyletron();
  const { t } = useTranslation();
  const {
    params: { channelId },
  } = matchPath('/channels/:channelId', pathname) || { params: {} };

  const onCreate = (channel) => {
    feedbackUtils.positive(t('channels.created.message'));
    setModalIsOpen(false);
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
          <Downloadable
            request={Channels.list.bind(Channels)}
            render={({ data: { items } }) =>
              items.map(({ id, name }) => (
                <DirectMessage
                  key={id}
                  id={id}
                  isActive={channelId === id}
                  name={name}
                />
              ))
            }
          />
        </ChannelListContainer>
        <ChannelListContainer
          title={t('explorer.direct')}
        ></ChannelListContainer>
      </div>
    </>
  );
};

export default Explorer;
