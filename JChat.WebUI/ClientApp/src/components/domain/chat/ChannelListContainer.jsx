import React from 'react';

import { styled } from 'baseui';
import { Button, KIND, SHAPE, SIZE } from 'baseui/button';
import { LabelMedium } from 'baseui/typography';

import Icon from '@/components/typography/Icon';

const StyledContainer = styled('div', {
  display: 'relative',
  maxWidth: '100%',
});

const StyledHeader = styled('div', {
  display: 'flex',
  justifyContent: 'space-between',
  alignItems: 'center',
  padding: '2px 5px 5px 5px',
});

const StyledBody = styled('div', {});

const ChannelListContainer = ({ title, children, onAdd }) => (
  <StyledContainer>
    <StyledHeader>
      <LabelMedium>{title}</LabelMedium>
      {onAdd && (
        <Button
          shape={SHAPE.circle}
          onClick={onAdd}
          size={SIZE.mini}
          kind={KIND.tertiary}
        >
          <Icon name='plus' />
        </Button>
      )}
    </StyledHeader>
    <StyledBody>{children}</StyledBody>
  </StyledContainer>
);

export default ChannelListContainer;
