import React from 'react';

import { styled, useStyletron } from 'baseui';

import ChatInfo from '@/components/domain/chat/ChatInfo';
import Messages from '@/components/domain/chat/Messages';
import Typewriter from '@/components/domain/chat/Typewriter';
import { Navigate, useParams } from 'react-router-dom';
import Downloadable from '@/components/data-display/Downloadable';
import { Channels } from '@/api';

const StyledContainer = styled('div', ({ $theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  width: '100%',
  position: 'relative',
  backgroundColor: $theme.colors.b2,
}));

const Chat = () => {
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
        render={({ data: { channel, messages } }) => (
          <>
            <ChatInfo name={channel.name} />
            <Messages
              messages={messages}
              className={css({
                flex: 1,
                overflowY: 'auto',
              })}
            />
            <Typewriter />
          </>
        )}
      />
    </StyledContainer>
  );
};

export default Chat;
