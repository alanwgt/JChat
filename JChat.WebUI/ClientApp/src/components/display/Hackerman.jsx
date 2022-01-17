import React from 'react';
import { useStyletron } from 'baseui';

import FullScreen from '@/components/display/FullScreen';
import THEME from '@/styles/theme';

const Hackerman = ({ children, ...props }) => {
  const [css] = useStyletron();

  return (
    <FullScreen
      $center
      $style={{
        backgroundImage: 'url(/assets/images/hackerman.png)',
        backgroundSize: 'cover',
        backgroundRepeat: 'repeat',
        backgroundPosition: 'center',
      }}
      {...props}
    >
      <div
        className={css({
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          flexDirection: 'column',
          borderRadius: THEME.borderRadius,
          backgroundColor: '#212121CC',
          padding: '30px',
          backdropFilter: 'blur(5px)',
          maxWidth: '400px',
        })}
      >
        {children}
      </div>
    </FullScreen>
  );
};

export default Hackerman;
