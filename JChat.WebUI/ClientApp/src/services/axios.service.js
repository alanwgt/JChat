import Axios from 'axios';
import store from '@/store';

const axios = Axios.create({
  withCredentials: true,
});

axios.interceptors.request.use((config) => {
  const newConf = { ...config };
  const id = store.getState().workspace?.id;

  if (id) {
    newConf.headers['X-Workspace-Id'] = id;
  }

  return newConf;
});

export default axios;
