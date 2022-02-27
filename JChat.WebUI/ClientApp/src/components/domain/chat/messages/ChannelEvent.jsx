import React from 'react';

import { useTranslation } from 'react-i18next';

import Divider from '@/components/data-display/Divider';

const ChannelEvent = ({ message }) => {
  const [t] = useTranslation();
  let title;

  switch (message.body) {
    case 'channels.user.joined':
      title = t('channels.user.joined', message.meta);
      break;
    default:
      title = message.body;
  }
  return <Divider title={title} color='accent' />;
};

export default ChannelEvent;
