import { createSelector } from 'reselect';

export const bootSelector = (state) => state.boot;

export const permissionsSelector = createSelector(
  bootSelector,
  (boot) => boot.permissions
);

export const workspaceUsersSelector = createSelector(
  bootSelector,
  (boot) => boot.users
);
