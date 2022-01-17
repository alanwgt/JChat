import React from 'react';
import { useStyletron } from 'baseui';
import { useTranslation } from 'react-i18next';
import THEME from '@/styles/theme';

const Typewriter = ({ ...props }) => {
  const { t } = useTranslation();
  const [css] = useStyletron();

  return (
    <div
      className={css({
        height: '20px',
      })}
      {...props}
    >
      {/*<Input*/}
      {/*  placeholder={t('typewriter.placeholder')}*/}
      {/*  className={css({*/}
      {/*    borderTopLeftRadius: '5px',*/}
      {/*    borderTopRightRadius: '5px',*/}
      {/*    borderBottom: '1px dashed rgba(153, 153, 153, 0.4)',*/}
      {/*  })}*/}
      {/*/>*/}
      <div
        className={css({
          borderBottomLeftRadius: '5px',
          borderBottomRightRadius: '5px',
          border: '1px solid rgba(153, 153, 153, 0.4)',
          borderTop: 'none',
          padding: THEME.inputPadding,
        })}
      >
        test
      </div>
    </div>
  );
};

export default Typewriter;
