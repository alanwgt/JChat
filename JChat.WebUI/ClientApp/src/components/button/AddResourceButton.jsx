import React from 'react';

import { Button, KIND, SIZE } from 'baseui/button';

import Icon from '@/components/typography/Icon';

const AddResourceButton = ({ children, ...props }) => (
  <Button
    kind={KIND.tertiary}
    size={SIZE.mini}
    startEnhancer={() => <Icon name='plus' />}
    {...props}
  >
    {children}
  </Button>
);

export default AddResourceButton;
