import React from 'react';
import { useStyletron } from 'baseui';
import THEME from '@/styles/theme';
import Messages from '@/components/domain/chat/Messages';

const StarredMessages = () => {
  const [css] = useStyletron();

  return (
    <div
      className={css({
        backgroundColor: THEME.colors.black,
        width: '33vw',
        minWidth: '200px',
        overflowY: 'auto',
      })}
    >
      <Messages />
    </div>
  );
};

export default StarredMessages;
