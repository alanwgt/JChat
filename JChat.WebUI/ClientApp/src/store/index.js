import { applyMiddleware, createStore } from 'redux';
import rootReducer from '@/store/rootReducer';
import {
  createSignalRConnection,
  signalRMiddleware,
} from '@/services/signalr.service';

const store = createStore(
  rootReducer,
  {},
  applyMiddleware(signalRMiddleware({ connection: createSignalRConnection() }))
);

export default store;
