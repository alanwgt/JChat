import React from 'react';
import { useTranslation } from 'react-i18next';
// eslint-disable-next-line no-unused-vars
import { Session } from '@ory/kratos-client';

import FullscreenSpinner from '@/components/feedback/FullscreenSpinner';
import * as Auth from '@/services/auth.service';

const AuthContext = React.createContext({
  login: () => {},
  logout: () => {},
  isAuthenticated: false,
  user: null,
});

export const AuthProvider = ({ children, ...props }) => {
  const [t] = useTranslation();

  /**
   * @property {Session|null} userData
   */
  const [userData, setUserData] = React.useState(null);
  const [authenticaded, setAuthenticated] = React.useState(null);

  const login = (userDataResponse) => {
    if (!userDataResponse || !userDataResponse.identity) {
      throw new Error('Unkexpected login data!');
    }

    setUserData(userDataResponse);
    setAuthenticated(true);
  };

  const logout = () =>
    Auth.logout().then(() => {
      setUserData(null);
      setAuthenticated(false);
    });

  React.useEffect(() => {
    Auth.checkAuthStatus()
      .then(({ data }) => {
        setAuthenticated(true);
        setUserData(data);
      })
      .catch((err) => {
        if (!err.response?.status && err.response.status !== 401) {
          throw err;
        }

        setAuthenticated(false);
      });
  }, []);

  if (authenticaded === null) {
    return <FullscreenSpinner message={t('auth.checking')} />;
  }

  return (
    <AuthContext.Provider
      value={{
        login,
        logout,
        isAuthenticated: authenticaded,
        user: userData,
      }}
      {...props}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => React.useContext(AuthContext);
