import React from 'react';
import { styled } from 'baseui';
import THEME from '@/styles/theme';

const Container = styled('div', {
  backgroundColor: THEME.colors.black,
  width: '200px',
  overflowY: 'auto',
});

const StarredMessages = () => <Container>sadasd asdas asdasd</Container>;

export default StarredMessages;
