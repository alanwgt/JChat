import Type from 'prop-types';
import { styled } from 'baseui';
import THEME from '@/styles/theme';

const DotStatus = styled('span', ({ $online }) => ({
  display: 'block',
  width: '8px',
  height: '8px',
  borderRadius: '50%',
  backgroundColor: $online ? THEME.colors.green : THEME.colors.red,
}));

DotStatus.propTypes = {
  $online: Type.bool,
};

DotStatus.defaultProps = {
  $online: false,
};

export default DotStatus;
