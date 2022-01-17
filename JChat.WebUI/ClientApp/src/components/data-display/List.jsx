import React from 'react';
import { ListItem } from 'baseui/list';

const List = ({ items, render }) => (
  <ul>
    {items.map((item) =>
      // <ListItem key={item.id || item.key}>{render(item)}</ListItem>
      render(item)
    )}
  </ul>
);

export default List;
