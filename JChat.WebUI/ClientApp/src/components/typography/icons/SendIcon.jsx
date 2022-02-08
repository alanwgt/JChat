import React from 'react';

import SvgWrapper from '@/components/typography/icons/SvgWrapper';

const SendIcon = ({ width = '24px', ...props }) => (
  <SvgWrapper width={width}>
    <svg viewBox='0 0 24 24' width='24' height='24' {...props}>
      <path
        fill='currentColor'
        d='M1.101 21.757 23.8 12.028 1.101 2.3l.011 7.912 13.623 1.816-13.623 1.817-.011 7.912z'
      />
    </svg>
  </SvgWrapper>
);

export default SendIcon;
