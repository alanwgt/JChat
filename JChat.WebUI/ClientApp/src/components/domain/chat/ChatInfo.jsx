import React from 'react';
import Type from 'prop-types';
import { styled } from 'baseui';

import UserStatus from '@/components/domain/chat/UserStatus';
import THEME from '@/styles/theme';

const Container = styled('div', {
  backgroundColor: THEME.colors.b3,
  display: 'flex',
  alignItems: 'center',
  padding: THEME.padding,
});

const ChatInfo = ({ name, ...props }) => (
  <Container {...props}>
    <UserStatus name={name} />
  </Container>
);

export default ChatInfo;
