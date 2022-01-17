import React from 'react';
import {
  Modal,
  ModalButton,
  ModalFooter,
  ModalHeader,
  ROLE,
} from 'baseui/modal';
import { useTranslation } from 'react-i18next';
import { KIND } from 'baseui/button';

const UserSettingsModal = () => {
  const [isOpen, setIsOpen] = React.useState(false);

  const [t] = useTranslation();
  return (
    <Modal
      autoFocus
      animate
      closeable
      onClose={() => {
        setIsOpen(false);
      }}
      role={ROLE.dialog}
      isOpen={isOpen}
      unstable_ModalBackdropScroll
    >
      <ModalHeader>{t('user.settings.header')}</ModalHeader>
      <ModalFooter>
        <ModalButton kind={KIND.tertiary}>{t('button.close')}</ModalButton>
      </ModalFooter>
    </Modal>
  );
};

export default UserSettingsModal;
