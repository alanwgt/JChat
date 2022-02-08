import React from 'react';

import { styled } from 'baseui';
import {LabelXSmall } from 'baseui/typography';
import Type from 'prop-types';

import DotStatus from '@/components/domain/chat/DotStatus';
import UserAvatar from '@/components/user/UserAvatar';

const Container = styled('div', {
  display: 'flex',
});

const AvatarStatusContainer = styled('div', {
  position: 'relative',
  marginRight: '5px',
});

const StyledLabel4 = styled(LabelXSmall, {
  display: 'flex',
  alignItems: 'center',
});

const UserStatus = ({ name, online, ...props }) => (
  <Container {...props}>
    <AvatarStatusContainer>
      <UserAvatar name={name} />
      <DotStatus
        $style={{
          position: 'absolute',
          bottom: 0,
          right: 0,
        }}
        $online={online}
      />
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
