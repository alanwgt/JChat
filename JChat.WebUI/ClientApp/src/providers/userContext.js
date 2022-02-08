import React from 'react';

import { useAuth } from '@/providers/authContext';

const UserData = {
  user: null,
  id: null,
  username: null,
  firstName: null,
  lastName: null,
  createdAt: null,
};

const UserContext = React.createContext({
  ...UserData,
});

export const UserProvider = ({ children, ...props }) => {
  const { user } = useAuth();

  const userData = React.useMemo(() => {
    if (user && user.identity) {
      const { identity } = user;

      return {
        user: identity,
        id: identity.id,
        username: identity.traits.username,
        firstName: identity.traits.name.first,
        lastName: identity.traits.name.last,
        createdAt: identity.created_at,
      };
    }

    return { ...UserData };
  }, [user]);

  return (
    <UserContext.Provider value={userData} {...props}>
      {children}
    </UserContext.Provider>
  );
};

export const useUser = () => React.useContext(UserContext);
