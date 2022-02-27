import React from 'react';

import { styled } from 'baseui';
import { Button, SIZE as ButtonSize, KIND, SHAPE } from 'baseui/button';
import { Input, SIZE } from 'baseui/input';
import { useTranslation } from 'react-i18next';

import { CreateMessageCommand } from '@/api/web-api-client';
import Icon from '@/components/typography/Icon';
import SendIcon from '@/components/typography/icons/SendIcon';
import MessageBodyType from '@/constants/messageBodyType';
import MessagePriority from '@/constants/messagePriority';
import { selectGif } from '@/utils/input.utils';

const TypewriterContainer = styled('div', ({ $theme }) => ({
  backgroundColor: $theme.colors.b3,
  padding: '2px 17px 2px 10px',
  boxShadow: $theme.shadow3inverted,
  display: 'flex',
  alignItems: 'center',
  // height: '20px',
}));

const Typewriter = ({ isLoading, onSubmit, ...props }) => {
  const [inputText, setInputText] = React.useState('');
  const { t } = useTranslation();

  const canSendMessage = () => !!inputText && !!inputText.trim().length;
  const sendMessage = (bodyTypeId, priorityId, meta = '') => {
    if (bodyTypeId === MessageBodyType.Text && !canSendMessage()) {
      return;
    }

    const command = new CreateMessageCommand({
      body: inputText,
      bodyType: bodyTypeId,
      priority: priorityId,
      meta,
    });

    Promise.resolve(onSubmit(command)).then(() => {
      setInputText('');
    });
  };

  const onKeyDown = (evt) => {
    if (evt.key !== 'Enter') {
      return;
    }

    sendMessage();
  };

  return (
    <TypewriterContainer {...props}>
      <Input
        clearable
        size={SIZE.mini}
        placeholder={t('typewriter.placeholder')}
        value={inputText}
        onKeyDown={onKeyDown}
        onChange={({ currentTarget: { value } }) => {
          setInputText(value);
        }}
        overrides={{
          Root: {
            style: {
              margin: '2px 5px',
            },
          },
        }}
      />
      <Button
        isLoading={isLoading}
        size={ButtonSize.compact}
        shape={SHAPE.pill}
        kind={KIND.tertiary}
        onClick={() => {
          sendMessage(MessageBodyType.Text, MessagePriority.Normal);
        }}
      >
        <SendIcon />
      </Button>
      <Button
        isLoading={isLoading}
        size={ButtonSize.compact}
        shape={SHAPE.pill}
        kind={KIND.tertiary}
        onClick={() => {
          selectGif()
            .then((gif) => {
              sendMessage(
                MessageBodyType.Gif,
                MessagePriority.Normal,
                JSON.stringify(gif)
              );
            })
            .catch(() => {});
        }}
      >
        <Icon name='dev' />
      </Button>
    </TypewriterContainer>
  );
};

export default Typewriter;
