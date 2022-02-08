import React from 'react';

import { Button, SIZE as ButtonSize, KIND } from 'baseui/button';
import { connect } from 'react-redux';

import Badge from '@/components/data-display/Badge';
import Icon from '@/components/typography/Icon';
import { setNotificationVisibility } from '@/store/ui/ui.actions';
import { notificationsSelector } from '@/store/ui/ui.selectors';

const NotificationsToggle = ({ notifications, openNotifications }) => (
  <Button
    kind={KIND.tertiary}
    size={ButtonSize.compact}
    onClick={() => openNotifications()}
    $style={{
      position: 'relative',
    }}
  >
    <Icon name='notification' />
    <Badge count={notifications.length} />
  </Button>
);

const mapState = (state) => ({
  notifications: notificationsSelector(state),
});

const mapDispatch = (dispatch) => ({
  openNotifications: () => dispatch(setNotificationVisibility(true)),
});

export default connect(mapState, mapDispatch)(NotificationsToggle);
