import React from 'react';

import { Button, KIND, SIZE } from 'baseui/button';
import { ChevronDown, ChevronUp } from 'baseui/icon';
import { StatefulMenu } from 'baseui/menu';
import { PLACEMENT, StatefulPopover, TRIGGER_TYPE } from 'baseui/popover';
import { LabelXSmall } from 'baseui/typography';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';

import { Workspaces } from '@/api';
import GlobalUserFinderModal from '@/components/domain/user/GlobalUserFinderModal';
import Icon from '@/components/typography/Icon';
import { useAuth } from '@/providers/authContext';
import { usePermissions } from '@/providers/permissionsContext';
import { useUser } from '@/providers/userContext';
import { deselectWorkspace } from '@/store/workspace/workspace.actions';
import { workspaceSelector } from '@/store/workspace/workspace.selectors';
import feedbackUtils from '@/utils/feedback.utils';

const MenuActionType = {
  Logout: 0,
  ChangeWorkspace: 1,
  InviteToWorkspace: 2,
};

const UserMenu = ({ workspace, changeWorkspace }) => {
  const [isOpen, setIsOpen] = React.useState(false);
  const [finderIsOpen, setFinderIsOpen] = React.useState(false);
  const can = usePermissions();
  const navigate = useNavigate();
  const [t] = useTranslation();
  const { logout } = useAuth();
  const { username } = useUser();

  const userActions = [
    {
      label: t('workspaces.change'),
      action: MenuActionType.ChangeWorkspace,
    },
    { label: t('auth.logout'), action: MenuActionType.Logout },
  ];

  if (can('manage', 'workspaces', workspace.id)) {
    userActions.push({
      label: t('workspaces.invite'),
      action: MenuActionType.InviteToWorkspace,
    });
  }

  return (
    <>
      <GlobalUserFinderModal
        isOpen={finderIsOpen}
        setIsOpen={setFinderIsOpen}
        onSelect={(users) => {
          const promises = [];
          users.forEach(({ id, name, username }) => {
            promises.push(
              Workspaces.invite({ userId: id, workspaceId: workspace.id })
            );
          });

          Promise.all(promises)
            .then(() => {
              setFinderIsOpen(false);
              feedbackUtils.positive(t('workspaces.invite.sent'));
            })
            .catch(feedbackUtils.error);
        }}
      />
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
            items={userActions}
            onItemSelect={({ item }) => {
              // eslint-disable-next-line default-case
              switch (item.action) {
                case MenuActionType.Logout:
                  logout();
                  break;
                case MenuActionType.ChangeWorkspace:
                  navigate('/');
                  changeWorkspace();
                  break;
                case MenuActionType.InviteToWorkspace:
                  setFinderIsOpen(true);
                  break;
              }
              close();
            }}
          />
        )}
      >
        <Button kind={KIND.tertiary} size={SIZE.mini} $style={{ gap: '2.5px' }}>
          <Icon name='user-alt-1' />
          <LabelXSmall>{username}</LabelXSmall>
          {isOpen ? <ChevronUp size={24} /> : <ChevronDown size={24} />}
        </Button>
      </StatefulPopover>
    </>
  );
};

const mapState = (state) => ({
  workspace: workspaceSelector(state),
});

const mapDispatch = (dispatch) => ({
  changeWorkspace: () => dispatch(deselectWorkspace()),
});

export default connect(mapState, mapDispatch)(UserMenu);
