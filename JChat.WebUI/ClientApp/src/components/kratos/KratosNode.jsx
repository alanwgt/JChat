import React from 'react';
import { FormControl } from 'baseui/form-control';
import { Controller } from 'react-hook-form';
// eslint-disable-next-line no-unused-vars
import { UiNode } from '@ory/kratos-client';
import { Input } from 'baseui/input';
import { useTranslation } from 'react-i18next';
import { Paragraph4 } from 'baseui/typography';

/**
 * @param {UiNode} node
 * @returns {JSX.Element}
 * @constructor
 */
const KratosNode = ({ control, error, node }) => {
  const [t] = useTranslation();
  let errorMessage = error?.type || null;

  if (node.messages.length) {
    errorMessage = node.messages.reduce(
      (acc, curr) => `${acc}\n${curr.text}`,
      ''
    );
  }

  return (
    <FormControl
      label={t(`kratos.${node.attributes.name}`)}
      disabled={node.attributes.disabled}
      error={errorMessage}
    >
      <Controller
        control={control}
        name={node.attributes.name}
        value={node.attributes.value}
        rules={{ required: true || node.attributes.required }}
        render={({ field }) => (
          <Input
            error={!!errorMessage}
            type={node.attributes.type}
            {...field}
          />
        )}
      />
    </FormControl>
  );
};

export default KratosNode;
