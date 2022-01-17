import React from 'react';
import { styled } from 'baseui';

import Message from '@/components/domain/chat/Message';

const MessageContainer = styled('div', {
  // display: 'flex',
  // flexDirection: 'column',
  paddingBottom: '8px',
});

const Messages = ({ ...props }) => {
  return (
    <MessageContainer {...props}>
      <Message>message 00</Message>
      <Message incoming>Message 01</Message>
      <Message>message 02</Message>
      <Message incoming>Message 02</Message>
      <Message>Message 02</Message>
      <Message>MESSAGE 02</Message>
      <Message>Message 02</Message>
      <Message>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus ante
        sem, aliquam non quam sed, tincidunt blandit metus. Pellentesque
        elementum, quam a venenatis aliquam, enim ex ullamcorper eros, vel
        auctor nisi purus in arcu. Ut ultrices, ligula non tristique lacinia,
        ante velit elementum metus, in porta purus mi eget dolor. Sed a placerat
        purus, et accumsan tellus. Vivamus facilisis lorem et turpis consequat,
        in convallis massa sodales.{' '}
      </Message>
      <Message incoming>Message 02</Message>
      <Message incoming>Message 02</Message>
      <Message incoming>Message 02</Message>
      <Message>
        Praesent feugiat magna sit amet elit viverra, quis sollicitudin leo
        vehicula. In nunc nunc, pellentesque molestie erat eget, commodo blandit
        turpis.
      </Message>
      <Message incoming>Message 02</Message>
      <Message>Message 02</Message>
      <Message>Message 02</Message>
      <Message>Message 02</Message>
      <Message incoming>MESSAGE 02</Message>
      <Message>Message 02</Message>
      <Message incoming>
        Nullam et elit rhoncus nisi congue feugiat. Sed a facilisis tellus, eget
        commodo massa. Nunc accumsan arcu massa, sed lobortis elit porta eu
      </Message>
      <Message incoming>Message 02</Message>
      <Message>Message 02</Message>
      <Message>Message 02</Message>
      <Message incoming>Message 02</Message>
      <Message>Message 02</Message>
      <Message>Message 02</Message>
    </MessageContainer>
  );
};

export default Messages;
