import React from 'react';

import ChannelEvent from '@/components/domain/chat/messages/ChannelEvent';
import GifMessage from '@/components/domain/chat/messages/GifMessage';
import MessageBubble from '@/components/domain/chat/messages/MessageBubble';
import TextMessage from '@/components/domain/chat/messages/TextMessage';
import MessageBodyType from '@/constants/messageBodyType';

const MessageFactory = ({ message }) => {
  const msg = { ...message };

  if (msg.meta) {
    msg.meta = JSON.parse(msg.meta);
  }

  switch (msg.bodyType.id) {
    case MessageBodyType.ChannelEvent:
      return <ChannelEvent message={msg} />;
    case MessageBodyType.Gif:
      return (
        <MessageBubble inbound={message.isInbound}>
          <GifMessage message={msg} />
        </MessageBubble>
      );
    default:
      return (
        <MessageBubble inbound={message.isInbound}>
          <TextMessage message={msg} />
        </MessageBubble>
      );
  }
};

export default MessageFactory;
