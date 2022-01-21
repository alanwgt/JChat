import React from 'react';

import { Button } from 'baseui/button';
import { ButtonGroup, SIZE } from 'baseui/button-group';
import { Paragraph4 } from 'baseui/typography';
import { useTranslation } from 'react-i18next';

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
      <ButtonGroup
        size={SIZE.large}
        overrides={{
          Root: {
            style: ({ $theme }) => ({
              gap: $theme.padding,
            }),
          },
        }}
      >
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
        >
          {t('auth.register')}
        </Button>
      </ButtonGroup>
    </Hackerman>
  );
};

export default ChooseAuth;
