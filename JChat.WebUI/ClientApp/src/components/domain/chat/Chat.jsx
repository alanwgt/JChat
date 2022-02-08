import React from 'react';

import { styled, useStyletron } from 'baseui';
import { Navigate, useParams } from 'react-router-dom';

import { Channels } from '@/api';
import { CreateMessageCommand } from '@/api/web-api-client';
import Downloadable from '@/components/data-display/Downloadable';
import ChatInfo from '@/components/domain/chat/ChatInfo';
import Messages from '@/components/domain/chat/Messages';
import Typewriter from '@/components/domain/chat/Typewriter';
import feedbackUtils from '@/utils/feedback.utils';

const StyledContainer = styled('div', ({ $theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  width: '100%',
  position: 'relative',
  backgroundColor: $theme.colors.b2,
}));

const Chat = () => {
  const [loading, setLoading] = React.useState(true);
  const [messages, setMessages] = React.useState([]);
  const { channelId } = useParams();
  const [css] = useStyletron();

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
          setMessages(data.messages);
        }}
        render={({ data: { channel } }) => (
          <>
            <ChatInfo channelId={channel.id} name={channel.name} />
            <Messages
              messages={messages}
              className={css({
                flex: 1,
                overflowY: 'auto',
              })}
            />
            <Typewriter
              isLoading={loading}
              onSubmit={(input) => {
                setLoading(true);
                const command = new CreateMessageCommand({
                  body: input,
                  bodyType: '87159cfd-1db5-42ec-9250-084b3fc41964',
                  priority: '195348d7-5de2-465c-ab01-89e76b7823cf',
                });
                return Channels.sendMessage(command, channelId)
                  .then((message) => {
                    setMessages([message, ...messages]);
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
