import React from 'react';

import { Input } from 'baseui/input';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Channels } from '@/api';
import FormInput from '@/components/data-entry/form/FormInput';
import Modal from '@/components/modal/Modal';
import feedbackUtils from '@/utils/feedback.utils';

const CreateChannelModal = ({ onCreate, ...props }) => {
  const [t] = useTranslation();
  const { handleSubmit, control } = useForm();

  const onSubmit = (data) => {
    Channels.create(data).then(onCreate).catch(feedbackUtils.error);
  };

  return (
    <Modal
      title={t('channels.modal.create.title')}
      {...props}
      onPositive={handleSubmit(onSubmit)}
      unstable_ModalBackdropScroll
    >
      <form>
        <FormInput
          name='name'
          formControl={control}
          label='channels.modal.input.name'
          rules={{ minLength: 3, maxLength: 50, required: true }}
          render={({ field, hasError }) => (
            <Input
              {...field}
              error={hasError}
              placeholder={t('channels.modal.input.name.placeholder')}
            />
          )}
        />
      </form>
    </Modal>
  );
};

export default CreateChannelModal;
