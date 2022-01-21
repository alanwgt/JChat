import React from 'react';

import { StyledSpinnerNext } from 'baseui/spinner';
import { Display4 } from 'baseui/typography';

import FullScreen from '@/components/display/FullScreen';

const FullscreenSpinner = ({ message = null }) => (
  <FullScreen $center $style={{ flexDirection: 'column' }}>
    {message && (
      <Display4 $style={{ marginBottom: '20px' }}>{message}</Display4>
    )}
    <StyledSpinnerNext size={100} />
  </FullScreen>
);

export default FullscreenSpinner;
