import React from 'react';

import { styled, useStyletron } from 'baseui';

import ChatInfo from '@/components/domain/chat/ChatInfo';
import Messages from '@/components/domain/chat/Messages';
import Typewriter from '@/components/domain/chat/Typewriter';

const Container = styled('div', ({ $theme }) => ({
  display: 'flex',
  flexDirection: 'column',
  width: '100%',
  position: 'relative',
  backgroundColor: $theme.colors.b2,
}));

const Chat = () => {
  const [css] = useStyletron();

  return (
    <Container>
      <ChatInfo name='test' />
      <Messages
        className={css({
          flex: 1,
          overflowY: 'auto',
        })}
      />
      <Typewriter />
    </Container>
  );
};

export default Chat;
