import React from 'react';
import Type from 'prop-types';
import { styled } from 'baseui';
import { LabelSmall } from 'baseui/typography';

import THEME from '@/styles/theme';

const MessageContainer = styled('div', ({ $incoming }) => ({
  marginBottom: '2px',
  paddingRight: '9%',
  paddingLeft: '9%',
  display: 'flex',
  justifyContent: $incoming ? 'flex-start' : 'flex-end',
}));

const StyledMessage = styled('div', ({ $incoming }) => ({
  maxWidth: '65%',
  padding: '6px 7px 6px 9px',
  borderRadius: THEME.borderRadius,
  width: 'fit-content',
  boxShadow: '0 1px .5px rgba(0,0,0,.13)',
  verticalAlign: 'baseline',
  ...($incoming ? THEME.incomingMessage : THEME.outgoingMessage),
}));

const TextContainer = styled('div', {
  position: 'relative',
  whiteSpace: 'pre-warp',
  overflowWrap: 'break-word',
});

const Text = styled(LabelSmall, {
  userSelect: 'text',
});

const Message = ({ children, incoming, reaction, ...props }) => (
  <MessageContainer $incoming={incoming}>
    <StyledMessage $incoming={incoming}>
      <TextContainer>
        <Text>{children}</Text>
      </TextContainer>
    </StyledMessage>
  </MessageContainer>
);

Message.propTypes = {
  children: Type.element.isRequired,
  reaction: Type.element,
  incoming: Type.bool,
};

Message.defaultProps = {
  reaction: null,
  incoming: false,
};

export default Message;
