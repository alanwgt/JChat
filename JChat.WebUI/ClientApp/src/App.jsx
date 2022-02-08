import React from 'react';

import { setConfig } from 'react-hot-loader';
import { hot } from 'react-hot-loader/root';
import { useSelector } from 'react-redux';

import SignalRHub from '@/components/domain/SignalRHub';
import WorkspaceSelector from '@/components/domain/workspace/WorkspaceSelector';
import { useAuth } from '@/providers/authContext';
import { isWorkspaceSelectedSelector } from '@/store/workspace/workspace.selectors';

setConfig({ logLevel: 'debug' });

const AuthenticatedApp = React.lazy(() => import('./AuthenticatedApp'));
const UnauthenticatedApp = React.lazy(() => import('./UnauthenticatedApp'));

const App = () => {
  const isWorkspaceSelected = useSelector(isWorkspaceSelectedSelector);
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return <UnauthenticatedApp />;
  }

  return (
    <>
      <SignalRHub />
      {isWorkspaceSelected ? <AuthenticatedApp /> : <WorkspaceSelector />}
    </>
  );
};

export default hot(App);
