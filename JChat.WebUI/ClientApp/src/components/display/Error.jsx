import React from 'react';
import { Display4, Paragraph3 } from 'baseui/typography';
import { useTranslation } from 'react-i18next';
import { KIND, Notification } from 'baseui/notification';

const Error = ({ err }) => {
  const [t] = useTranslation();
  const isResponse = !!err.response;

  const defaultTitle = t('exception.unknown');
  const defaultBody = err.message;

  const title = isResponse
    ? err.response.data?.error?.message ?? defaultTitle
    : defaultTitle;
  const body = isResponse
    ? err.response.data?.error?.reason ?? defaultBody
    : defaultBody;

  return (
    <Notification kind={KIND.negative}>
      <Display4>{title}</Display4>
      <Paragraph3>{body}</Paragraph3>
    </Notification>
  );
};

export default Error;
