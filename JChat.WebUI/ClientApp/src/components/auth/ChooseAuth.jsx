import React from 'react';
import { Paragraph4 } from 'baseui/typography';
import { ButtonGroup, SIZE } from 'baseui/button-group';
import { Button } from 'baseui/button';
import { useTranslation } from 'react-i18next';

import THEME from '@/styles/theme';
import Hackerman from '@/components/display/Hackerman';
import LogoTitle from '@/components/layout/LogoTitle';
import HackedText from '@/components/typography/HackedText';

export const AuthAction = {
  SignIn: 1,
  SignUp: 2,
};

const ChooseAuth = ({ onSelect }) => {
  const [t] = useTranslation();

  return (
    <Hackerman>
      <LogoTitle title={<HackedText>{t('auth.header.choose')}</HackedText>} />
      <Paragraph4
        $style={{
          margin: `20px auto`,
        }}
      >
        {t('auth.description')}
      </Paragraph4>
      <ButtonGroup size={SIZE.large}>
        <Button
          onClick={() => {
            onSelect(AuthAction.SignIn);
          }}
        >
          {t('auth.login')}
        </Button>
        <Button
          onClick={() => {
            onSelect(AuthAction.SignUp);
          }}
          $style={{
            marginLeft: THEME.padding,
          }}
        >
          {t('auth.register')}
        </Button>
      </ButtonGroup>
    </Hackerman>
  );
};

export default ChooseAuth;
