import React from 'react';

import { ParagraphSmall } from 'baseui/typography';

import Modal from '@/components/modal/Modal';

const ConfirmModal = ({
  title,
  description,
  onConfirm,
  onCancel,
  ...props
}) => {
  const [isOpen, setIsOpen] = React.useState(true);

  if (!isOpen) {
    return null;
  }

  return (
    <Modal
      title={title}
      {...props}
      isOpen={isOpen}
      setIsOpen={setIsOpen}
      onPositive={onConfirm}
      onNegative={onCancel}
      unstable_ModalBackdropScroll
    >
      <ParagraphSmall>{description}</ParagraphSmall>
    </Modal>
  );
};

export default ConfirmModal;
