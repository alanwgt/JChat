import { createSelector } from 'reselect';

export const workspaceSelector = (state) => state.workspace;

export const isWorkspaceSelectedSelector = createSelector(
  workspaceSelector,
  (workspace) => !!workspace.id
);
