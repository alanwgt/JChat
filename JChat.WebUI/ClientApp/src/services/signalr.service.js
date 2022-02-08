// @link https://medium.com/@lucavgobbi/signalr-react-redux-5a100a226871
import {
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel,
} from '@microsoft/signalr';

import { NotificationType } from '@/api/web-api-client';
import i18n from '@/i18n';
import Console from '@/services/logger.service';
import { addChannel } from '@/store/boot/boot.actions';
import { addNotification } from '@/store/ui/ui.actions';
import feedbackUtils from '@/utils/feedback.utils';

let connection = null;
let connectionPromise = null;

const isConnected = () =>
  !!connection && connection.state === HubConnectionState.Connected;

export const signalRMiddleware = () => (store) => (next) => (action) => {
  if (!isConnected()) {
    Console.debug('chat hub still disconnected, ignoring middleware');
    return next(action);
  }

  return typeof action === 'function'
    ? action(
        store.dispatch.bind(store),
        store.getState.bind(store),
        connection.invoke.bind(connection)
      )
    : next(action);
};

export const createSignalRConnection = () => {
  connection = new HubConnectionBuilder()
    .configureLogging(
      process.env.NODE_ENV === 'production'
        ? LogLevel.Information
        : LogLevel.Debug
    )
    .withAutomaticReconnect()
    .withUrl('http://api.jchat.alanwgt.com/hub')
    .build();
};

export const startConnection = (store) => {
  connectionPromise = connection.start().then(() => {
    Console.debug(`signalR connection established`);
    connection.onclose = () => {
      Console.debug('server requested to close connection');
    };
    connection.on('pong', () => {
      Console.info('received pong');
    });
    connection.on('NewNotification', (notification) => {
      const meta = JSON.parse(notification.meta);
      Console.debug(`received notification ${JSON.stringify(notification)}`);

      switch (notification.type) {
        case NotificationType.WorkspaceInvitation:
          feedbackUtils.info(i18n.t('worspaces.invite.received'));
          break;
        case NotificationType.UserJoinedWorkspace:
          feedbackUtils.info(
            i18n.t('workspaces.user.joined', { username: meta.username })
          );
          break;
        case NotificationType.UserJoinedChannel:
          feedbackUtils.info(
            i18n.t('channels.added', { name: meta.channelName })
          );
          store.dispatch(addChannel(meta));
          break;
        default:
      }

      store.dispatch(addNotification(notification));
    });
    connection.on('ConnectedToWorkspace', (workspaceId) => {
      Console.debug(`connected to workspace ${workspaceId}`);
    });
    connection.on('DisconnectedFromWorkspace', (workspaceId) => {
      Console.debug(`disconnected from workspace ${workspaceId}`);
    });
  });
  return connectionPromise;
};

export const connectToWorkspace = (workspaceId) => {
  Console.debug(`connecting to workspace ${workspaceId}`);
  if (!isConnected()) {
    return Promise.resolve(connectionPromise).then(() =>
      connection.send('connectToWorkspace', workspaceId)
    );
  }

  return connection.send('connectToWorkspace', workspaceId);
};

export const disconnectFromWorkspace = (workspaceId) => {
  Console.debug(`disconnecting from workspace ${workspaceId}`);
  return connection.send('disconnectFromWorkspace', workspaceId);
};

export const stopConnection = () => connection.stop();
