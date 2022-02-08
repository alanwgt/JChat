import React from 'react';

import { StyledSpinnerNext } from 'baseui/spinner';
import { DisplaySmall } from 'baseui/typography';

import FullScreen from '@/components/display/FullScreen';

const FullscreenSpinner = ({ message = null }) => (
  <FullScreen $center $style={{ flexDirection: 'column' }}>
    {message && (
      <DisplaySmall $style={{ marginBottom: '20px' }}>{message}</DisplaySmall>
    )}
    <StyledSpinnerNext size={100} />
  </FullScreen>
);

export default FullscreenSpinner;
