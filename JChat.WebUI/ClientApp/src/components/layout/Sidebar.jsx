import React from 'react';

import { styled } from 'baseui';

import Explorer from '@/components/domain/chat/Explorer';

const Container = styled('div', {
  overflowY: 'auto',
});

const StyledExplorer = styled(Explorer, ({ $theme }) => ({
  backgroundColor: $theme.colors.black,
  width: '250px',
}));

const Sidebar = () => (
  <Container>
    <StyledExplorer />
  </Container>
);

export default Sidebar;
