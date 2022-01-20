import React from 'react';

import EmptyList from '@/components/data-display/list/EmptyList';

const List = ({ items, render }) => {
  if (!items || !items.length) {
    return <EmptyList />;
  }

  return (
    <ul>
      {items.map((item) =>
        // <ListItem key={item.id || item.key}>{render(item)}</ListItem>
        render(item)
      )}
    </ul>
  );
};

export default List;
