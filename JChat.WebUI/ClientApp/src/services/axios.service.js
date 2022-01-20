import Axios from 'axios';

import store from '@/store';

const axios = Axios.create({
  withCredentials: true,
});

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

export default axios;
