import { memoize } from 'lodash';
import { createSelector } from 'reselect';

export const bootSelector = (state) => state.boot;

export const permissionsSelector = createSelector(
  bootSelector,
  (boot) => boot.permissions
);

export const channelsSelector = createSelector(
  bootSelector,
  (boot) => boot.channels
);

export const workspaceUsersSelector = createSelector(
  bootSelector,
  (boot) => boot.users
);

export const hasPermissionSelector = createSelector(
  permissionsSelector,
  (permissions) =>
    memoize(
      (relation, ns, objId) =>
        !!permissions.find(
          (p) =>
            p.namespace === ns && p.object === objId && p.relation === relation
        )
    )
);
