import React from 'react';
import { KIND, Notification } from 'baseui/notification';

import KratosForm from '@/components/kratos/KratosForm';
import Hackerman from '@/components/display/Hackerman';
import LogoTitle from '@/components/layout/LogoTitle';

const KratosResponseForm = ({
  data: { id, ui },
  onSubmit,
  title,
  ...props
}) => (
  <Hackerman>
    <LogoTitle title={title} />
    {ui.messages?.length &&
      ui.messages.map((msg) => (
        <Notification kind={KIND.negative} key={msg.id} closeable>
          {msg.text}
        </Notification>
      ))}
    <KratosForm
      ui={ui}
      {...props}
      onSubmit={(data) => {
        onSubmit(id, data);
      }}
    />
  </Hackerman>
);

export default KratosResponseForm;
