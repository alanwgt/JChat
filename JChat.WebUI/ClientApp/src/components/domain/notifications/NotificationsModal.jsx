import React from 'react';

import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';

import { Notifications } from '@/api';
import { NotificationType } from '@/api/web-api-client';
import List from '@/components/data-display/list/List';
import WorkspaceInvitationEvent from '@/components/domain/notifications/byEvent/WorkspaceInvitationEvent';
import Modal from '@/components/modal/Modal';
import {
  setNotificationVisibility,
  setNotifications,
} from '@/store/ui/ui.actions';
import { notificationsSelector } from '@/store/ui/ui.selectors';
import feedbackUtils from '@/utils/feedback.utils';

const NotificationsModal = ({
  notifications,
  setNotifications: setNotificationItems,
  isOpen,
  setIsOpen,
  ...props
}) => {
  const [t] = useTranslation();

  React.useEffect(() => {
    Notifications.list()
      .then(({ items }) => {
        setNotificationItems(items);
      })
      .catch(feedbackUtils.error);
  }, []);

  return (
    <Modal
      closeable={false}
      title={t('notifications.title')}
      isOpen={isOpen}
      setIsOpen={setIsOpen}
      onPositive={() => {
        setIsOpen(false);
      }}
      {...props}
    >
      <List
        items={notifications}
        render={(notification) => {
          switch (notification.type) {
            case NotificationType.WorkspaceInvitation:
              return <WorkspaceInvitationEvent notification={notification} />;
            default:
              return (
                <p key={notification.id}>{JSON.stringify(notification.meta)}</p>
              );
          }
        }}
      />
    </Modal>
  );
};

const mapState = (state) => ({
  isOpen: state.ui.isNotificationsOpen,
  notifications: notificationsSelector(state),
});

const mapDispatch = (dispatch) => ({
  setIsOpen: (open) => dispatch(setNotificationVisibility(open)),
  setNotifications: (notifications) =>
    dispatch(setNotifications(notifications)),
});

export default connect(mapState, mapDispatch)(NotificationsModal);
