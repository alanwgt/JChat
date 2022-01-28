import React from 'react';

import { styled } from 'baseui';
import Type from 'prop-types';

import UserStatus from '@/components/domain/chat/UserStatus';
import { useNavigate } from 'react-router-dom';
import { expandStyleIf } from '@/utils/styletron.utils';

const DM = styled('div', ({ $isActive, $theme }) => ({
  display: 'flex',
  paddingLeft: '5px',
  ':not(:last-of-type)': {
    marginBottom: '5px',
  },
  ...$theme.textBreak,
  ...$theme.clickable,
  ...expandStyleIf($isActive, {
    outline: `1px dashed ${$theme.colors.themePrimary}`,
    backgroundColor: `${$theme.colors.themePrimary}4D`,
  }),
}));

const DirectMessage = ({ id, name, isActive = false, ...props }) => {
  const navigate = useNavigate();

  return (
    <DM
      onClick={() => {
        navigate(`channels/${id}`);
      }}
      $isActive={isActive}
      {...props}
    >
      <UserStatus name={name} />
    </DM>
  );
};

DirectMessage.propTypes = {
  name: Type.string.isRequired,
};

export default DirectMessage;
