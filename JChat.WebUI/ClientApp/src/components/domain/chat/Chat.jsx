import React from 'react';

import { styled } from 'baseui';
import { useDispatch, useSelector } from 'react-redux';
import { Navigate, useParams } from 'react-router-dom';

import { Channels } from '@/api';
import Downloadable from '@/components/data-display/Downloadable';
import ChatInfo from '@/components/domain/chat/ChatInfo';
import Messages from '@/components/domain/chat/Messages';
import Typewriter from '@/components/domain/chat/Typewriter';
import {
  addChannelMessage,
  setChannelMessages,
} from '@/store/chat/chat.actions';
import { channelMessagesSelector } from '@/store/chat/chat.selectors';
import feedbackUtils from '@/utils/feedback.utils';

const StyledContainer = styled('div', ({ $theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  width: '100%',
  position: 'relative',
  backgroundColor: $theme.colors.chatBackground,
}));

const Chat = () => {
  const [loading, setLoading] = React.useState(true);
  const { channelId } = useParams();
  const dispatch = useDispatch();
  const messages = useSelector(channelMessagesSelector)(channelId);

  if (!channelId) {
    return <Navigate to='/' replace />;
  }

  return (
    <StyledContainer>
      <Downloadable
        request={Channels.getChannelDetails.bind(Channels)}
        params={channelId}
        onResolve={(data) => {
          setLoading(false);
          dispatch(setChannelMessages(channelId, data.messages));
        }}
        render={({ data: { channel } }) => (
          <>
            <ChatInfo channelId={channel.id} name={channel.name} />
            <Messages messages={messages} />
            <Typewriter
              isLoading={loading}
              onSubmit={(command) => {
                setLoading(true);
                return Channels.sendMessage(command, channelId)
                  .then((message) => {
                    dispatch(addChannelMessage(channelId, message));
                  })
                  .catch((err) => {
                    feedbackUtils.error(err);
                    throw err;
                  })
                  .finally(() => {
                    setLoading(false);
                  });
              }}
            />
          </>
        )}
      />
    </StyledContainer>
  );
};

export default Chat;
