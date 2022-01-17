// @link https://medium.com/@lucavgobbi/signalr-react-redux-5a100a226871
import {
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel,
} from '@microsoft/signalr';
import Console from '@/services/logger.service';

export const signalRMiddleware =
  ({ callbacks, connection }) =>
  (store) =>
  (next) =>
  (action) => {
    if (connection.state === HubConnectionState.Disconnected) {
      Console.debug({
        label: 'signalr',
        message: 'chat hub still disconnected, ignoring middleware',
      });
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

export const createSignalRConnection = () =>
  new HubConnectionBuilder()
    .configureLogging(
      process.env.NODE_ENV === 'production'
        ? LogLevel.Information
        : LogLevel.Debug
    )
    .withUrl('http://api.jchat.alanwgt.com/hub')
    .build();
