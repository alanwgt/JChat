import React from 'react';
import { styled } from 'baseui';

const Container = styled('div', {});

const Collapsible = ({ children,...props }) => {
  const [open, setOpen] = React.useState(false);

  const toggleState = () => {
    setOpen(!open);
  };

  return (
    <Container>

      {open && (
        {children}
      )}
    </Container>
  )
};

export default Collapsible;
