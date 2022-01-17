import React from 'react';
import Type from 'prop-types';
import { styled } from 'baseui';
import { Label4 } from 'baseui/typography';

import THEME from '@/styles/theme';
import DotStatus from '@/components/domain/chat/DotStatus';
import UserAvatar from '@/components/user/UserAvatar';

const Container = styled('div', {
  display: 'flex',
});

const AvatarStatusContainer = styled('div', {
  position: 'relative',
  marginRight: '5px',
});

const Status = styled(DotStatus, {
  position: 'absolute',
  bottom: 0,
  right: 0,
});

const StyledLabel4 = styled(Label4, {
  display: 'flex',
  alignItems: 'center',
});

const UserStatus = ({ name, online, ...props }) => (
  <Container {...props}>
    <AvatarStatusContainer>
      <UserAvatar name={name} />
      <Status $online={online} />
    </AvatarStatusContainer>
    <StyledLabel4>{name}</StyledLabel4>
  </Container>
);

UserStatus.propTypes = {
  name: Type.string.isRequired,
  online: Type.bool,
};

UserStatus.defaultProps = {
  online: false,
};

export default UserStatus;
