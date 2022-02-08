import React from 'react';

import { styled } from 'baseui';
import { LabelXSmall } from 'baseui/typography';

const StyledContainer = styled('div', ({ $theme, $color }) => ({
  backgroundColor: $theme.colors[$color],
  position: 'absolute',
  width: '14px',
  height: '14px',
  top: '0',
  right: '0',
  borderRadius: '50%',
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center',
}));

const StyledCounter = styled('div', {});

const Badge = ({ count = null, color = 'red' }) =>
  count && count > 0 ? (
    <StyledContainer $color={color}>
      <StyledCounter>
        <LabelXSmall>{count}</LabelXSmall>
      </StyledCounter>
    </StyledContainer>
  ) : null;

export default Badge;
