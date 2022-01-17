import React from 'react';

const HackedText = ({ children }) => (
  <span className='hacked' data-text={children}>
    {children}
  </span>
);

export default HackedText;
