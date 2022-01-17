import React from 'react';
import { connect } from 'react-redux';
import { PLACEMENT, StatefulPopover, TRIGGER_TYPE } from 'baseui/popover';
import { StatefulMenu } from 'baseui/menu';
import { ChevronDown, ChevronUp } from 'baseui/icon';
import { Button, KIND, SIZE } from 'baseui/button';
import { Label4 } from 'baseui/typography';
import { useTranslation } from 'react-i18next';

import { useUser } from '@/providers/userContext';
import { useAuth } from '@/providers/authContext';
import { deselectWorkspace } from '@/store/workspace/workspace.actions';

const MenuActionType = {
  Logout: 0,
  ChangeWorkspace: 1,
};

const UserMenu = ({ changeWorkspace }) => {
  const [isOpen, setIsOpen] = React.useState(false);
  const [t] = useTranslation();
  const { logout } = useAuth();
  const { username } = useUser();

  return (
    <StatefulPopover
      autoFocus={false}
      dismissOnEsc
      dismissOnClickOutside
      onOpen={() => setIsOpen(true)}
      onClose={() => setIsOpen(false)}
      placement={PLACEMENT.bottomLeft}
      triggerType={TRIGGER_TYPE.click}
      content={({ close }) => (
        <StatefulMenu
          items={[
            {
              label: t('workspace.change'),
              action: MenuActionType.ChangeWorkspace,
            },
            { label: t('auth.logout'), action: MenuActionType.Logout },
          ]}
          onItemSelect={({ item }) => {
            // eslint-disable-next-line default-case
            switch (item.action) {
              case MenuActionType.Logout:
                logout();
                break;
              case MenuActionType.ChangeWorkspace:
                changeWorkspace();
                break;
            }
            close();
          }}
        />
      )}
    >
      <Button kind={KIND.tertiary} size={SIZE.mini}>
        <Label4>{username}</Label4>
        {isOpen ? <ChevronUp size={28} /> : <ChevronDown size={28} />}
      </Button>
    </StatefulPopover>
  );
};

const mapDispatch = (dispatch) => ({
  changeWorkspace: () => dispatch(deselectWorkspace()),
});

export default connect(null, mapDispatch)(UserMenu);
