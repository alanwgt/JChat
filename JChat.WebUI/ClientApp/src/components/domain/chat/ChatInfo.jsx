import React from 'react';

import { styled } from 'baseui';

import UserStatus from '@/components/domain/chat/UserStatus';

const Container = styled('div', ({ $theme }) => ({
  backgroundColor: $theme.colors.b3,
  display: 'flex',
  alignItems: 'center',
  padding: $theme.padding,
  boxShadow: $theme.shadow3,
}));

const ChatInfo = ({ name, ...props }) => (
  <Container {...props}>
    <UserStatus name={name} />
  </Container>
);

export default ChatInfo;
