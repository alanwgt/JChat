import React from 'react';

import SvgWrapper from '@/components/typography/icons/SvgWrapper';

const Logo = ({ width = '45px', ...props }) => (
  <SvgWrapper width={width}>
    <svg viewBox='0 0 45 81' {...props}>
      <g
        transform='matrix(5.792292138661224,0,0,5.792292138661224,0.0005786348357436782,-34.845842773224454)'
        fill='#FFF'
      >
        <path d='M4.9902 6.016 l2.6855 0 l0 4.5898 l-0.27344 0 l0 4.9902 l0.27344 0 l0 4.0137 l-6.4453 0 l0 -0.85938 l-1.2305 0 l0 -1.582 l0.74219 0 l0 -0.27344 l-0.74219 0 l0 -0.35156 l2.6465 0 l0 0.97656 l2.3633 0 l0 -1.3477 c0 -0.46224 -0.0065136 -1.097 -0.019531 -1.9043 l-2.0703 0 l0 -0.20508 l2.0703 0 l0 -8.0469 z M5.0098 14.0625 l0 0.20508 l0.79102 0 l0 -0.20508 l-0.79102 0 z M2.7148 19.81445 l3.4277 0 l0 0.18555 l-3.4277 0 l0 -0.18555 z' />
      </g>
    </svg>
  </SvgWrapper>
);

export default Logo;
