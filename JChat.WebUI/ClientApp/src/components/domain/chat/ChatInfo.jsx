import React from 'react';

import { styled } from 'baseui';
import { Button, KIND, SIZE } from 'baseui/button';
import { StatefulMenu } from 'baseui/menu';
import { PLACEMENT, StatefulPopover, TRIGGER_TYPE } from 'baseui/popover';
import { useTranslation } from 'react-i18next';

import { Channels } from '@/api';
import UserStatus from '@/components/domain/chat/UserStatus';
import GlobalUserFinderModal from '@/components/domain/user/GlobalUserFinderModal';
import Icon from '@/components/typography/Icon';
import { usePermissions } from '@/providers/permissionsContext';
import feedbackUtils from '@/utils/feedback.utils';

const MenuActionType = {
  InviteToChannel: 1,
};

const StyledContainer = styled('div', ({ $theme }) => ({
  backgroundColor: $theme.colors.b3,
  display: 'flex',
  alignItems: 'center',
  padding: $theme.padding,
  boxShadow: $theme.shadow3,
  justifyContent: 'space-between',
}));

const ChatInfo = ({ name, channelId, ...props }) => {
  const [finderIsOpen, setFinderIsOpen] = React.useState(false);
  const [t] = useTranslation();
  const can = usePermissions();
  const canManageChannel = can('manage', 'channels', channelId);

  const menuActions = [];

  if (canManageChannel) {
    menuActions.push({
      label: t('channels.add-member'),
      action: MenuActionType.InviteToChannel,
    });
  }

  return (
    <StyledContainer {...props}>
      <UserStatus name={name} />
      {canManageChannel && (
        <GlobalUserFinderModal
          scopeToWorkspace
          onSelect={(users) => {
            users.forEach(({ id: userId }) => {
              Channels.addMember({ userId }, channelId)
                .then(() => {
                  feedbackUtils.success(t('channels.invite.sent'));
                  setFinderIsOpen(false);
                })
                .catch(feedbackUtils.error);
            });
          }}
          isOpen={finderIsOpen}
          setIsOpen={setFinderIsOpen}
        />
      )}
      {menuActions.length && (
        <StatefulPopover
          autoFocus={false}
          dismissOnEsc
          dismissOnClickOutside
          placement={PLACEMENT.bottomLeft}
          triggerType={TRIGGER_TYPE.click}
          content={({ close }) => (
            <StatefulMenu
              items={menuActions}
              onItemSelect={({ item: { action } }) => {
                switch (action) {
                  case MenuActionType.InviteToChannel:
                    setFinderIsOpen(true);
                    break;
                }
                close();
              }}
            />
          )}
        >
          <Button
            kind={KIND.tertiary}
            size={SIZE.mini}
            $style={{ gap: '2.5px' }}
          >
            <Icon name='menu' />
          </Button>
        </StatefulPopover>
      )}
    </StyledContainer>
  );
};

export default ChatInfo;
