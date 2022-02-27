import React from 'react';

import { styled } from 'baseui';

const StyledMessage = styled('div', ({ $theme, $inbound }) => ({
  maxWidth: '65%',
  padding: '6px 7px 6px 9px',
  borderRadius: $theme.borderRadius,
  width: 'fit-content',
  boxShadow: '0 1px .5px rgba(0,0,0,.13)',
  verticalAlign: 'baseline',
  ...($inbound ? $theme.inboundMessage : $theme.outgoingMessage),
}));

const TextContainer = styled('div', {
  position: 'relative',
  whiteSpace: 'pre-warp',
  overflowWrap: 'break-word',
});

const MessageBubble = ({ children, inbound }) => (
  <StyledMessage $inbound={inbound}>
    <TextContainer>{children}</TextContainer>
  </StyledMessage>
);

export default MessageBubble;
