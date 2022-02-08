import {
  AddNotification,
  SetNotificationStatus,
  SetNotifications,
  ToggleNotifications,
  ToggleStarred,
} from '@/store/ui/ui.constants';

export const toggleStarredMessagesBar = () => ({
  type: ToggleStarred,
});

export const toggleNotificationsVisibility = () => ({
  type: ToggleNotifications,
});

export const setNotificationVisibility = (open) => ({
  type: SetNotificationStatus,
  payload: open,
});

export const setNotifications = (items) => ({
  type: SetNotifications,
  payload: items,
});

export const addNotification = (notification) => ({
  type: AddNotification,
  payload: notification,
});
