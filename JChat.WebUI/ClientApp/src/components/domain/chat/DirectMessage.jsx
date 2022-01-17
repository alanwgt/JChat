import React from 'react';
import Type from 'prop-types';
import { styled } from 'baseui';
import THEME from '@/styles/theme';
import UserStatus from '@/components/domain/chat/UserStatus';

const DM = styled('div', {
  display: 'flex',
  ':not(:last-of-type)': {
    marginBottom: '5px',
  },
  ...THEME.textBreak,
});

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
