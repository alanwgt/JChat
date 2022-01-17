import React from 'react';
import { ListItem as BaseListItem, ListItemLabel } from 'baseui/list';

const ListItem = ({ children, description = null, onClick = null }) => {
  let style = null;

  if (onClick) {
    style = () => ({
      ':hover': {
        cursor: 'pointer',
        backgroundColor: 'rgba(42, 41, 40, .7)',
      },
    });
  }

  return (
    <span
      onClick={() => {
        onClick?.();
      }}
    >
      <BaseListItem
        overrides={{
          Root: {
            style,
          },
        }}
      >
        <ListItemLabel description={description}>{children}</ListItemLabel>
      </BaseListItem>
    </span>
  );
};

export default ListItem;
