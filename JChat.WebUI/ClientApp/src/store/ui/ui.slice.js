import {
  AddNotification,
  SetNotificationStatus,
  SetNotifications,
  ToggleNotifications,
  ToggleStarred,
} from '@/store/ui/ui.constants';

const initialState = {
  isStarredMessagesBarOpen: false,
  isNotificationsOpen: false,
  notifications: [],
};

const uiSlice = (state = initialState, action) => {
  switch (action.type) {
    case ToggleStarred:
      return {
        ...state,
        isStarredMessagesBarOpen: !state.isStarredMessagesBarOpen,
      };
    case SetNotificationStatus:
      return {
        ...state,
        isNotificationsOpen: action.payload,
      };
    case ToggleNotifications:
      return {
        ...state,
        isNotificationsOpen: !state.isNotificationsOpen,
      };
    case SetNotifications:
      return {
        ...state,
        notifications: action.payload,
      };
    case AddNotification:
      return {
        ...state,
        notifications: [action.payload, ...state.notifications],
      };
    default:
      return state;
  }
};

export default uiSlice;
