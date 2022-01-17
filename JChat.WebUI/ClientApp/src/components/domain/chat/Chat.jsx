import React from 'react';
import { styled, useStyletron } from 'baseui';

import Messages from '@/components/domain/chat/Messages';
import Typewriter from '@/components/domain/chat/Typewriter';
import THEME from '@/styles/theme';
import ChatInfo from '@/components/domain/chat/ChatInfo';

const Container = styled('div', {
  display: 'flex',
  flexDirection: 'column',
  width: '100%',
  position: 'relative',
  backgroundColor: THEME.colors.b2,
});

const Chat = () => {
  const [css] = useStyletron();

  return (
    <Container>
      <ChatInfo
        name='test'
        className={css({
          boxShadow: THEME.shadow3,
        })}
      />
      <Messages
        className={css({
          flex: 1,
          overflowY: 'auto',
        })}
      />
      <Typewriter
        className={css({
          backgroundColor: THEME.colors.b3,
          padding: THEME.padding,
          boxShadow: THEME.shadow3inverted,
        })}
      />
    </Container>
  );
};

export default Chat;
