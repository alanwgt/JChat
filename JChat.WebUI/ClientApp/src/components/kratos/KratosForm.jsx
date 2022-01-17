import React from 'react';
import { useStyletron } from 'baseui';
import { Button, KIND } from 'baseui/button';
import { useForm } from 'react-hook-form';

import KratosNode from '@/components/kratos/KratosNode';
import { dotAccess } from '@/utils/object.utils';

const KratosForm = ({ ui, onCancel, onSubmit }) => {
  const [css] = useStyletron();
  const nodes = ui.nodes.filter(
    (node) =>
      node.attributes.type !== 'submit' && node.attributes.type !== 'hidden'
  );

  const {
    control,
    formState: { errors },
    handleSubmit,
  } = useForm({
    defaultValues: ui.nodes.reduce((acc, curr) => {
      const key = curr.attributes.name;

      if (key.includes('.')) {
        let ref = acc;
        const kSplit = key.split('.');
        kSplit.forEach((k, idx) => {
          if (idx === kSplit.length - 1) {
            ref[k] = curr.attributes.value || '';
            return;
          }

          if (!ref[k]) {
            ref[k] = {};
          }

          ref = ref[k];
        });
        return acc;
      }

      return {
        ...acc,
        [curr.attributes.name]: curr.attributes.value || '',
      };
    }, {}),
  });

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      {nodes.map((node) => (
        <KratosNode
          key={node.attributes.name}
          node={node}
          control={control}
          error={dotAccess(errors, node.attributes.name)}
        />
      ))}
      <div
        className={css({
          display: 'flex',
          justifyContent: 'space-between',
          flexDirection: 'row-reverse',
        })}
      >
        <Button kind={KIND.primary} type='submit'>
          submit
        </Button>
        <Button
          kind={KIND.tertiary}
          onClick={() => {
            onCancel?.();
          }}
        >
          cancel
        </Button>
      </div>
    </form>
  );
};

export default KratosForm;
