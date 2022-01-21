import React from 'react';

import FullscreenInput from '@/components/display/FullscreenInput';

const Hackerman = ({ children, ...props }) => (
  <FullscreenInput
    $style={{
      backgroundImage: 'url(/assets/images/hackerman.png)',
      backgroundSize: 'cover',
      backgroundRepeat: 'repeat',
      backgroundPosition: 'center',
    }}
    {...props}
  >
    {children}
  </FullscreenInput>
);

export default Hackerman;
