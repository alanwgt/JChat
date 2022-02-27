import { styled } from 'baseui';

const Backdrop = styled('div', ({ $theme }) => ({
  position: 'absolute',
  width: '100vw',
  height: '100vh',
  top: 0,
  left: 0,
  backgroundColor: $theme.colors.neutralDarkAlpha,
  backdropFilter: 'blur(5px)',
}));

export default Backdrop;
