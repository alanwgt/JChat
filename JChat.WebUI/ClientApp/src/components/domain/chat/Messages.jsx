import React, { useRef } from 'react';

import { useStyletron } from 'baseui';

import Message from '@/components/domain/chat/Message';

const Messages = ({ messages, ...props }) => {
  const [css] = useStyletron();
  const ref = useRef();

  React.useEffect(() => {
    ref.current.scrollIntoView({ behavior: 'smooth' });
  }, [messages.length]);

  return (
    <div
      {...props}
      className={css({
        flex: 1,
        overflowY: 'auto',
        paddingBottom: '15px',
      })}
    >
      {messages.map((m) => (
        <Message key={m.id} message={m} />
      ))}
      <span ref={ref} className={css({ display: 'hidden' })} />
    </div>
  );
};

export default Messages;
