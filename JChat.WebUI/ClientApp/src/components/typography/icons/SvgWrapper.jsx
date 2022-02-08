import React from 'react';

import { useStyletron } from 'baseui';

const SvgWrapper = ({ width, children, ...props }) => {
  const [css] = useStyletron();

  return (
    <div
      className={css({ width, display: 'flex', alignItems: 'center' })}
      {...props}
    >
      {children}
    </div>
  );
};

export default SvgWrapper;
