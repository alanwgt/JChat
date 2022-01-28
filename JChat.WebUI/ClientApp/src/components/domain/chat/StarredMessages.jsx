import React from 'react';

import { styled } from 'baseui';

import Messages from '@/components/domain/chat/Messages';

const MessageContainer = styled('div', ({ $theme }) => ({
  backgroundColor: $theme.colors.black,
  width: '33vw',
  minWidth: '200px',
  overflowY: 'auto',
}));

const StarredMessages = () => (
  <MessageContainer>
    <Messages messages={[]} />
  </MessageContainer>
);

export default StarredMessages;
