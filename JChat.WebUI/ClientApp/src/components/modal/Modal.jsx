import React from 'react';

import { KIND as ButtonKind } from 'baseui/button';
import {
  Modal as BaseUiModal,
  ModalBody,
  ModalButton,
  ModalFooter,
  ModalHeader,
  ROLE,
  SIZE,
} from 'baseui/modal';
import { useTranslation } from 'react-i18next';

const Modal = ({
  children,
  title,
  isOpen,
  setIsOpen,
  onNegative,
  onPositive,
  closeable = true,
  negativeText = null,
  positiveText = null,
}) => {
  const [t] = useTranslation();

  return (
    <BaseUiModal
      onClose={() => setIsOpen(false)}
      closeable={closeable}
      isOpen={isOpen}
      animate
      autoFocus
      size={SIZE.auto}
      role={ROLE.dialog}
    >
      {title && <ModalHeader>{title}</ModalHeader>}
      <ModalBody>{children}</ModalBody>
      <ModalFooter>
        {closeable && (
          <ModalButton
            kind={ButtonKind.tertiary}
            onClick={() => {
              if (onNegative) {
                onNegative();
              } else {
                setIsOpen(false);
              }
            }}
          >
            {negativeText || t('modal.negative.btn')}
          </ModalButton>
        )}
        <ModalButton onClick={() => onPositive?.()}>
          {positiveText || t('modal.positive.btn')}
        </ModalButton>
      </ModalFooter>
    </BaseUiModal>
  );
};

export default Modal;
