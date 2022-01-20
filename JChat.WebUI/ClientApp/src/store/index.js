import { applyMiddleware, compose, createStore } from 'redux';
import persistState from 'redux-localstorage';

import {
  createSignalRConnection,
  signalRMiddleware,
} from '@/services/signalr.service';
import rootReducer from '@/store/rootReducer';

const store = createStore(
  rootReducer,
  {},
  compose(
    applyMiddleware(
      signalRMiddleware({ connection: createSignalRConnection() })
    ),
    persistState('workspace')
  )
);

export default store;
