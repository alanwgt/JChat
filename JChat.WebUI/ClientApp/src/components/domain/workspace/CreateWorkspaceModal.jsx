import React from 'react';

import { Input } from 'baseui/input';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

import { Workspaces } from '@/api';
import FormInput from '@/components/data-entry/form/FormInput';
import Modal from '@/components/modal/Modal';
import feedbackUtils from '@/utils/feedback.utils';

const CreateWorkspaceModal = ({ onCreate, ...props }) => {
  const [t] = useTranslation();
  const { handleSubmit, control } = useForm();

  const onSubmit = (data) => {
    Workspaces.create(data).then(onCreate).catch(feedbackUtils.error);
  };

  return (
    <Modal
      title={t('worspace.modal.create.title')}
      {...props}
      onPositive={handleSubmit(onSubmit)}
    >
      <form>
        <FormInput
          name='name'
          formControl={control}
          label='workspace.modal.input.name'
          rules={{ minLength: 3, maxLength: 50, required: true }}
          render={({ field }, hasError) => (
            <Input
              {...field}
              error={hasError}
              placeholder={t('workspace.modal.input.name.placeholder')}
            />
          )}
        />
      </form>
    </Modal>
  );
};

export default CreateWorkspaceModal;
