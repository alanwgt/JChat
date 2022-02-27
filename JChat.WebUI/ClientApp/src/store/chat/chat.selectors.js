import { memoize } from 'lodash';
import { createSelector } from 'reselect';

export const chatSelector = (state) => state.chat;

export const channelsSelector = createSelector(
  chatSelector,
  (chat) => chat.channels
);

export const userStatusSelector = createSelector(
  chatSelector,
  (chat) => chat.userStatus
);

export const isUserOnlineSelector = createSelector(
  userStatusSelector,
  (userStatus) =>
    memoize((userId) => userStatus[userId] && userStatus[userId] === true)
);

export const channelsMessagesSelector = createSelector(
  chatSelector,
  (chat) => chat.channelMessages
);

export const channelMessagesSelector = createSelector(
  channelsMessagesSelector,
  (channelMessages) => memoize((channelId) => channelMessages[channelId] ?? [])
);
