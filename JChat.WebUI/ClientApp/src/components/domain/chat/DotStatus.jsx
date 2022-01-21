import { styled } from 'baseui';
import Type from 'prop-types';

const DotStatus = styled('span', ({ $theme, $online }) => ({
  display: 'block',
  width: '8px',
  height: '8px',
  borderRadius: '50%',
  backgroundColor: $online ? $theme.colors.green : $theme.colors.red,
}));

DotStatus.propTypes = {
  $online: Type.bool,
};

DotStatus.defaultProps = {
  $online: false,
};

export default DotStatus;
