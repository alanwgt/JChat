import React from 'react';

import { memoize, values } from 'lodash';
import { useSelector } from 'react-redux';

import { permissionsSelector } from '@/store/boot/boot.selectors';

const PermissionWeight = {
  ownership: 1000,
  manage: 800,
  member: 600,
  write: 400,
  read: 200,
};

const PermissionsContext = React.createContext({
  can: () => false,
});

export const PermissionsProvider = ({ children, ...props }) => {
  const permissions = useSelector(permissionsSelector);

  const can = memoize(
    (relation, ns, objId) =>
      !!permissions.find(
        (p) =>
          p.namespace === ns &&
          p.object === objId &&
          PermissionWeight[relation.toLowerCase()] <= // what I'm asking for
            PermissionWeight[p.relation] // what I have
      ),
    (...args) => values(args).join(':')
  );

  return (
    <PermissionsContext.Provider value={can} {...props}>
      {children}
    </PermissionsContext.Provider>
  );
};

export const usePermissions = () => React.useContext(PermissionsContext);
