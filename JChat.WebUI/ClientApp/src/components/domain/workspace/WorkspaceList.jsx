import React from 'react';

import List from '@/components/data-display/list/List';
import ListItem from '@/components/data-display/list/ListItem';

const WorkspaceList = ({ workspaces, onClick }) => (
  <List
    items={workspaces}
    render={(workspace) => (
      <ListItem
        key={workspace.id}
        onClick={() => {
          onClick?.(workspace);
        }}
      >
        {workspace.name}
      </ListItem>
    )}
  />
);

export default WorkspaceList;
