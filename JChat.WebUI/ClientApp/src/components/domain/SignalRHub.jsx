import React from 'react';

import { useStore } from 'react-redux';

import loggerService from '@/services/logger.service';
import {
  createSignalRConnection,
  startConnection,
  stopConnection,
} from '@/services/signalr.service';

const SignalRHub = () => {
  const store = useStore();

  React.useEffect(() => {
    createSignalRConnection();
    startConnection(store);

    return () => {
      stopConnection().then(() => {
        loggerService.debug('chat connection closed');
      });
    };
  }, []);

  return null;
};

export default SignalRHub;
