import React from 'react';

import { styled } from 'baseui';

import FullScreen from '@/components/display/FullScreen';

const Container = styled('div', ({ $theme }) => ({
  borderRadius: $theme.borderRadius,
  backgroundColor: $theme.colors.neutralDarkAlpha,
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center',
  flexDirection: 'column',
  padding: '30px',
  backdropFilter: 'blur(5px)',
  maxWidth: '400px',
}));

const FullscreenInput = ({ children, ...props }) => (
  <FullScreen
    $center
    $style={{
      backgroundColor: '#240b36',
      backgroundImage: 'linear-gradient(200deg, #c31432, #240b36)',
    }}
    {...props}
  >
    <Container>{children}</Container>
  </FullScreen>
);

export default FullscreenInput;
