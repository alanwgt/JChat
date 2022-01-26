import React from 'react';

import { hot } from 'react-hot-loader/root';
import { connect } from 'react-redux';

import WorkspaceSelector from '@/components/domain/workspace/WorkspaceSelector';
import { useAuth } from '@/providers/authContext';
import { isWorkspaceSelectedSelector } from '@/store/workspace/workspace.selectors';

const AuthenticatedApp = React.lazy(() => import('./AuthenticatedApp'));
const UnauthenticatedApp = React.lazy(() => import('./UnauthenticatedApp'));

const App = ({ isWorkspaceSelected }) => {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return <UnauthenticatedApp />;
  }

  if (!isWorkspaceSelected) {
    return <WorkspaceSelector />;
  }

  return <AuthenticatedApp />;
};

const mapState = (state) => ({
  isWorkspaceSelected: isWorkspaceSelectedSelector(state),
});

export default connect(mapState)(hot(App));
