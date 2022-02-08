import { applyMiddleware, compose, createStore } from 'redux';
import persistState from 'redux-localstorage';

import { signalRMiddleware } from '@/services/signalr.service';
import rootReducer from '@/store/rootReducer';

const combinedReducers = compose(
  applyMiddleware(signalRMiddleware()),
  persistState('workspace')
);

const store = createStore(rootReducer, {}, combinedReducers);

if (module.hot) {
  module.hot.accept('./rootReducer', () => {
    store.replaceReducer(combinedReducers);
  });
}

export default store;
