import React from 'react';

import { useSelector } from 'react-redux';

import { hasPermissionSelector } from '@/store/boot/boot.selectors';

const PermissionsContext = React.createContext({
  can: () => false,
});

export const PermissionsProvider = ({ children, ...props }) => {
  const permissionChecker = useSelector(hasPermissionSelector);

  const can = (relation, ns, objId) => permissionChecker(relation, ns, objId);

  return (
    <PermissionsContext.Provider value={can} {...props}>
      {children}
    </PermissionsContext.Provider>
  );
};

export const usePermissions = () => React.useContext(PermissionsContext);
