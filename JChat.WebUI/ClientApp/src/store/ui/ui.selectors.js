import { createSelector } from 'reselect';

export const uiSelector = (state) => state.ui;

export const isStarredMessagesBarOpenSelector = createSelector(
  uiSelector,
  (ui) => ui.isStarredMessagesBarOpen
);

export const notificationsSelector = createSelector(
  uiSelector,
  (ui) => ui.notifications
);
