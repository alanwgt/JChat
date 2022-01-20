import { styled } from 'baseui';

const FullScreen = styled('div', ({ $theme, $center = false }) => ({
  width: '100vw',
  height: '100vh',
  backgroundColor: $theme.colors.neutralDark,
  ...($center
    ? {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
      }
    : {}),
}));

export default FullScreen;
