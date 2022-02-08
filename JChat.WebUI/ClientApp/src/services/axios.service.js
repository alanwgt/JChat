import Axios from 'axios';
import _ from 'lodash';

import store from '@/store';
import feedbackUtils from '@/utils/feedback.utils';

const axios = Axios.create({
  withCredentials: true,
});

const showErrorMesssage = (message) =>
  _.debounce(() => {
    feedbackUtils.error(message);
  }, 2000);

axios.interceptors.request.use((config) => {
  // don't touch the request sent to kratos
  // TODO: get domain from env
  if (config.url.includes('kratos.jchat.alanwgt.com')) {
    return config;
  }

  const newConf = { ...config };
  const id = store.getState().workspace?.id;

  if (id) {
    newConf.headers['X-Workspace-Id'] = id;
  }

  return newConf;
});

axios.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.message.toLowerCase().includes('network error')) {
      showErrorMesssage('server offline');
      return Promise.reject(new Error('exceptions.server_offline'));
    }

    if (error.response && error.response.status === 403) {
      // eslint-disable-next-line no-param-reassign
      error.i18nKey = 'exceptions.http_status_codes.403';
      return Promise.reject(error);
    }

    return Promise.reject(error);
  }
);

export default axios;
