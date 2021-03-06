import React from 'react';

import { styled, useStyletron } from 'baseui';
import { Button, SIZE as ButtonSize, KIND } from 'baseui/button';
import { Input, SIZE } from 'baseui/input';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';

import UserMenu from '@/components/auth/UserMenu';
import UserSettingsModal from '@/components/auth/UserSettingsModal';
import NotificationsModal from '@/components/domain/notifications/NotificationsModal';
import NotificationsToggle from '@/components/domain/notifications/NotificationsToggle';
import Icon from '@/components/typography/Icon';
import Logo from '@/components/typography/icons/Logo';
import { toggleStarredMessagesBar } from '@/store/ui/ui.actions';
import { isStarredMessagesBarOpenSelector } from '@/store/ui/ui.selectors';

const Container = styled('div', ({ $theme }) => ({
  display: 'flex',
  backgroundColor: $theme.colors.themeDark,
  alignItems: 'center',
  padding: `5px ${$theme.padding}`,
  justifyContent: 'space-between',
  width: '100%',
  boxSizing: 'border-box',
}));

const Header = ({ isStarredMessagesBarOpen, toggleStarredMessagesBar }) => {
  const { t } = useTranslation();
  const [css] = useStyletron();

  return (
    <>
      <NotificationsModal />
      <UserSettingsModal />
      <Container>
        <div
          className={css({
            paddingLeft: '20px',
          })}
        >
          <Logo width='16px' />
        </div>
        {/*<div*/}
        {/*  className={css({*/}
        {/*    width: '400px',*/}
        {/*  })}*/}
        {/*>*/}
        {/*  <Input*/}
        {/*    size={SIZE.compact}*/}
        {/*    placeholder={t('global-search-placeholder')}*/}
        {/*  />*/}
        {/*</div>*/}
        <div>
          <NotificationsToggle />
          {/*<Button*/}
          {/*  kind={KIND.tertiary}*/}
          {/*  size={ButtonSize.compact}*/}
          {/*  onClick={() => toggleStarredMessagesBar()}*/}
          {/*>*/}
          {/*  <Icon name={isStarredMessagesBarOpen ? 'star-filled' : 'star'} />*/}
          {/*</Button>*/}
          <UserMenu />
        </div>
      </Container>
    </>
  );
};

const mapState = (state) => ({
  isStarredMessagesBarOpen: isStarredMessagesBarOpenSelector(state),
});

const mapDispatch = (dispatch) => ({
  toggleStarredMessagesBar: () => dispatch(toggleStarredMessagesBar()),
});

export default connect(mapState, mapDispatch)(Header);
