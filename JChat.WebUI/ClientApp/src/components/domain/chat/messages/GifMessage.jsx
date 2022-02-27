import React from 'react';

import { Gif } from '@giphy/react-components';
import { styled } from 'baseui';

import TextMessage from '@/components/domain/chat/messages/TextMessage';

const StyledContainer = styled('div', {});

const GifMessage = ({ message }) => (
  <StyledContainer>
    <Gif gif={message.meta} width='100%' height='100%' noLink />
    <TextMessage message={message} />
  </StyledContainer>
);

export default GifMessage;
