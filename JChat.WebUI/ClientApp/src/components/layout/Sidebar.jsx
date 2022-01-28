import React from 'react';

import { styled } from 'baseui';

import Explorer from '@/components/domain/chat/Explorer';

const Container = styled('div', ({ $theme }) => ({
  overflowY: 'auto',
  backgroundColor: $theme.colors.black,
  width: '250px',
  maxWidth: '250px',
  display: 'block',
}));

const Sidebar = () => (
  <Container>
    <Explorer />
  </Container>
);

export default Sidebar;
