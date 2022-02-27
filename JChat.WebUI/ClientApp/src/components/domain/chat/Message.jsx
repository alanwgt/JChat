import React from 'react';

import { styled } from 'baseui';

import MessageFactory from '@/components/domain/chat/messages/MessageFactory';

const MessageContainer = styled('div', ({ $inbound }) => ({
  marginBottom: '2px',
  paddingRight: '9%',
  paddingLeft: '9%',
  display: 'flex',
  justifyContent: $inbound ? 'flex-start' : 'flex-end',
}));

const Message = ({ message, ...props }) => (
  <MessageContainer $inbound={message.isInbound} {...props}>
    <MessageFactory message={message} />
  </MessageContainer>
);

export default Message;
