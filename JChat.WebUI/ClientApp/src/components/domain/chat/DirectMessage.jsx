import React from 'react';

import { styled } from 'baseui';
import Type from 'prop-types';

import UserStatus from '@/components/domain/chat/UserStatus';

const DM = styled('div', ({ $theme }) => ({
  display: 'flex',
  ':not(:last-of-type)': {
    marginBottom: '5px',
  },
  ...$theme.textBreak,
}));

const DirectMessage = ({ name }) => {
  return (
    <DM>
      <UserStatus name={name} />
    </DM>
  );
};

DirectMessage.propTypes = {
  name: Type.string.isRequired,
};

export default DirectMessage;
