import React from 'react';

import { Button } from 'baseui/button';
import { ButtonGroup, SIZE } from 'baseui/button-group';
import { useTranslation } from 'react-i18next';

import { Workspaces } from '@/api';
import ListItem from '@/components/data-display/list/ListItem';
import Icon from '@/components/typography/Icon';
import feedbackUtils from '@/utils/feedback.utils';

const WorkspaceInvitationEvent = ({ notification }) => {
  const [t] = useTranslation();
  const { workspaceId, workspaceName, invitationId } = JSON.parse(
    notification.meta
  );

  return (
    <ListItem
      key={notification.id}
      endEnhancer={() => (
        <ButtonGroup size={SIZE.compact}>
          <Button
            onClick={() => {
              Workspaces.acceptInvite({ invitationId }).then(({}) => {
                feedbackUtils.positive(
                  t('notifications.workspace.invite.accepted')
                );
              });
            }}
          >
            <Icon name='checkmark' />
          </Button>
          <Button onClick={() => {}}>
            <Icon name='close' />
          </Button>
        </ButtonGroup>
      )}
    >
      {t('notifications.workspace.invite', {
        name: workspaceName,
        invitedBy: notification.createdBy,
      })}
    </ListItem>
  );
};

export default WorkspaceInvitationEvent;
