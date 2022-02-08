import React from 'react';

import EmptyList from '@/components/data-display/list/EmptyList';

const List = ({ items, render, ...props }) => {
  if (!items || !items.length) {
    return <EmptyList />;
  }

  return <ul {...props}>{items.map((item) => render(item))}</ul>;
};

export default List;
