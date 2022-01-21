import React from 'react';

import { styled } from 'baseui';
import { useTranslation } from 'react-i18next';

const TypewriterContainer = styled('div', ({ $theme }) => ({
  backgroundColor: $theme.colors.b3,
  padding: $theme.padding,
  boxShadow: $theme.shadow3inverted,
  height: '20px',
}));

const Typewriter = ({ ...props }) => {
  const { t } = useTranslation();

  return (
    <TypewriterContainer {...props}>
      {/*<Input*/}
      {/*  placeholder={t('typewriter.placeholder')}*/}
      {/*  className={css({*/}
      {/*    borderTopLeftRadius: '5px',*/}
      {/*    borderTopRightRadius: '5px',*/}
      {/*    borderBottom: '1px dashed rgba(153, 153, 153, 0.4)',*/}
      {/*  })}*/}
      {/*/>*/}
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
