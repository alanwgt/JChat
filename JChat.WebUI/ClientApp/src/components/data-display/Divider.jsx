import React from 'react';

import { styled, useStyletron } from 'baseui';
import { LabelXSmall } from 'baseui/typography';

const StyledContainer = styled('div', {
  position: 'relative',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
  width: '100%',
  margin: '10px 0',
});

const StyledHr = styled('span', ({ $theme, $color }) => ({
  width: '100%',
  height: '1px',
  backgroundColor: $color ? $theme.colors[$color] : 'white',
}));

const Divider = ({ title = null, color = null }) => {
  const [css, theme] = useStyletron();

  return (
    <StyledContainer>
      <StyledHr $color={color} />
      {title && (
        <LabelXSmall
          className={css({
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            position: 'absolute',
            padding: '2px 15px',
            backgroundColor: theme.colors.chatBackground,
            color: color ? theme.colors[color] : null,
          })}
        >
          {title}
        </LabelXSmall>
      )}
    </StyledContainer>
  );
};

export default Divider;
