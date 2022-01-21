import React from 'react';

import { styled, useStyletron } from 'baseui';
import { Input, SIZE } from 'baseui/input';
import { useTranslation } from 'react-i18next';

import UserMenu from '@/components/auth/UserMenu';
import UserSettingsModal from '@/components/auth/UserSettingsModal';
import Logo from '@/components/typography/icons/Logo';

const Container = styled('div', ({ $theme }) => ({
  display: 'flex',
  backgroundColor: $theme.colors.themeDark,
  alignItems: 'center',
  padding: `5px ${$theme.padding}`,
  justifyContent: 'space-between',
  width: '100%',
  boxSizing: 'border-box',
}));

const Header = () => {
  const { t } = useTranslation();
  const [css] = useStyletron();

  return (
    <>
      <UserSettingsModal />
      <Container>
        <Logo width='16px' />
        <div
          className={css({
            width: '400px',
          })}
        >
          <Input
            size={SIZE.compact}
            placeholder={t('global-search-placeholder')}
          />
        </div>
        <UserMenu />
      </Container>
    </>
  );
};

export default Header;
