import React from 'react';

import { styled, useStyletron } from 'baseui';
import { Button, SIZE as ButtonSize, KIND, SHAPE } from 'baseui/button';
import { Input, SIZE } from 'baseui/input';
import { useTranslation } from 'react-i18next';

import SendIcon from '@/components/typography/icons/SendIcon';

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
  const [css] = useStyletron();

  const canSendMessage = () => !!inputText && !!inputText.trim().length;

  return (
    <TypewriterContainer {...props}>
      <Input
        clearable
        size={SIZE.mini}
        placeholder={t('typewriter.placeholder')}
        value={inputText}
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
        disabled={isLoading || !canSendMessage()}
        isLoading={isLoading}
        size={ButtonSize.compact}
        shape={SHAPE.pill}
        kind={KIND.tertiary}
        onClick={() => {
          if (!canSendMessage()) {
            return;
          }

          Promise.resolve(onSubmit(inputText)).then(() => {
            setInputText('');
          });
        }}
      >
        <SendIcon />
      </Button>
      {/*<div*/}
      {/*  className={css({*/}
      {/*    borderBottomLeftRadius: '5px',*/}
      {/*    borderBottomRightRadius: '5px',*/}
      {/*    border: '1px solid rgba(153, 153, 153, 0.4)',*/}
      {/*    borderTop: 'none',*/}
      {/*    padding: THEME.inputPadding,*/}
      {/*  })}*/}
      {/*>*/}
      {/*  test*/}
      {/*</div>*/}
    </TypewriterContainer>
  );
};

export default Typewriter;
