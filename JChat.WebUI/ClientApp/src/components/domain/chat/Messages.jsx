import React from 'react';

import Message from '@/components/domain/chat/Message';
import { useStyletron } from 'baseui';

const Messages = ({ messages, ...props }) => {
  const [css] = useStyletron();

  return (
    <div
      className={css({
        paddingBottom: '8px',
      })}
      {...props}
    >
      {messages.map((m) => (
        <Message>{m}</Message>
      ))}
    </div>
  );
};

export default Messages;
