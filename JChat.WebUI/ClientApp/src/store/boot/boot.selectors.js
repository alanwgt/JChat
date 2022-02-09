import { memoize } from 'lodash';
import { createSelector } from 'reselect';

export const bootSelector = (state) => state.boot;

const PermissionWeight = {
  ownership: 1000,
  manage: 800,
  member: 600,
  write: 400,
  read: 200,
};

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
            p.namespace === ns &&
            p.object === objId &&
            PermissionWeight[relation.toLowerCase()] <= // what I'm asking for
              PermissionWeight[p.relation] // what I have
        )
    )
);
