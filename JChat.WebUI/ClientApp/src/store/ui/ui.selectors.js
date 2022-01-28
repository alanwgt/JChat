import { createSelector } from 'reselect';

export const uiSelector = (state) => state.ui;

export const isStarredMessagesBarOpenSelector = createSelector(
  uiSelector,
  (ui) => ui.isStarredMessagesBarOpen
);
