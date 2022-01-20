import React from 'react';

import { FormControl } from 'baseui/form-control';
import { Controller } from 'react-hook-form';
import { useTranslation } from 'react-i18next';

const FormInput = ({
  label,
  formControl,
  render,
  name,
  value = undefined,
  disabled = false,
  error = null,
  rules = {},
}) => {
  const [t] = useTranslation();
  const [fieldError, setFieldError] = React.useState(error);

  return (
    <FormControl label={t(label)} disabled={disabled} error={fieldError}>
      <Controller
        control={formControl}
        render={(props) => {
          const { fieldState } = props;

          if (fieldState.invalid) {
            setFieldError(
              `${name} ${t(`form.input.error.${fieldState.error.type}`, {
                [fieldState.error.type]: rules[fieldState.error.type],
              })}`
            );
          }

          return render(props, !!fieldError);
        }}
        rules={rules}
        name={name}
        value={value}
      />
    </FormControl>
  );
};

export default FormInput;
