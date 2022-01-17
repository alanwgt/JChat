// eslint-disable-next-line import/extensions,import/no-unresolved
import * as ApiDeclaration from './web-api-client';
import { axios } from '@/services/api';

export const Channel = new ApiDeclaration.ChannelClient(
  'http://api.jchat.alanwgt.com',
  axios
);
export const Hook = new ApiDeclaration.HookClient(
  'http://api.jchat.alanwgt.com',
  axios
);
export const Test = new ApiDeclaration.TestClient(
  'http://api.jchat.alanwgt.com',
  axios
);
export const Workspaces = new ApiDeclaration.WorkspacesClient(
  'http://api.jchat.alanwgt.com',
  axios
);
