import React from 'react';

import { ParagraphSmall } from 'baseui/typography';

const TextMessage = ({ message }) => (
  <ParagraphSmall>{message.body}</ParagraphSmall>
);

export default TextMessage;
