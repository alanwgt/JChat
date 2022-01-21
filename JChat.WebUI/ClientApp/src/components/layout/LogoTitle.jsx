import React from 'react';

import { useStyletron } from 'baseui';
import { Display4 } from 'baseui/typography';

import Logo from '@/components/typography/icons/Logo';

const LogoTitle = ({ title, ...props }) => {
  const [css] = useStyletron();

  return (
    <div
      className={css({
        display: 'flex',
        marginBottom: '15px',
      })}
      {...props}
    >
      <Logo width='24px' />
      <Display4
        $style={{
          marginLeft: '20px',
        }}
      >
        {title}
      </Display4>
    </div>
  );
};

export default LogoTitle;
