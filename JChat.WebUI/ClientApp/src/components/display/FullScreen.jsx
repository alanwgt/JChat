import { styled } from 'baseui';

const FullScreen = styled('div', ({ $center = false }) => ({
  width: '100vw',
  height: '100vh',
  ...($center
    ? {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
      }
    : {}),
}));

export default FullScreen;
