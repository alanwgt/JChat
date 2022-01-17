import React from 'react';
import List from '@/components/data-display/List';
import ListItem from '@/components/data-display/ListItem';

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
