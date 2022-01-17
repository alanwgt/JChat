import React from 'react';
import { connect } from 'react-redux';
import { hot } from 'react-hot-loader/root';

import { useAuth } from '@/providers/authContext';
import { isWorkspaceSelectedSelector } from '@/store/workspace/workspace.selectors';
import paginatedRemoteDataHoc from '@/hocs/paginatedRemoteDataHoc';
import WorkspaceSelector from '@/components/domain/workspace/WorkspaceSelector';
import { Workspaces } from '@/api';

const AuthenticatedApp = React.lazy(() => import('./AuthenticatedApp'));
const UnauthenticatedApp = React.lazy(() => import('./UnauthenticatedApp'));

const WorkspaceSelectorWithData = paginatedRemoteDataHoc(WorkspaceSelector);

const App = ({ isWorkspaceSelected }) => {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return <UnauthenticatedApp />;
  }

  if (!isWorkspaceSelected) {
    return (
      <WorkspaceSelectorWithData
        request={Workspaces.list.bind(Workspaces)}
        dataKey='workspaces'
      />
    );
  }

  return <AuthenticatedApp />;
};

const mapState = (state) => ({
  isWorkspaceSelected: isWorkspaceSelectedSelector(state),
});

export default connect(mapState)(hot(App));
