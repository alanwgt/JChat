import React from 'react';
import Type from 'prop-types';

import { useStyletron } from 'baseui';
import { Avatar } from 'baseui/avatar';
import { expandBorderStyles } from 'baseui/styles';

const UserAvatar = ({ name, src, size }) => {
  const [css] = useStyletron();

  return (
    <Avatar
      name={name}
      size={size}
      src={src}
      overrides={{
        Root: {
          style: ({ $theme }) => ({
            ...expandBorderStyles($theme.borders.border500),
          }),
        },
      }}
    />
  );
};

UserAvatar.propTypes = {
  name: Type.string.isRequired,
  size: Type.string,
  src: Type.string,
};

UserAvatar.defaultProps = {
  size: 'scale800',
  src: null,
};

export default UserAvatar;
