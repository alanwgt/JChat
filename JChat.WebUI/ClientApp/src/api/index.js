// eslint-disable-next-line import/extensions,import/no-unresolved
import axios from '@/services/axios.service';

import * as ApiDeclaration from './web-api-client';

export const Boot = new ApiDeclaration.BootClient(
  'http://api.jchat.alanwgt.com',
  axios
);
export const Channels = new ApiDeclaration.ChannelsClient(
  'http://api.jchat.alanwgt.com',
  axios
);
export const Hook = new ApiDeclaration.HookClient(
  'http://api.jchat.alanwgt.com',
  axios
);
export const Users = new ApiDeclaration.UsersClient(
  'http://api.jchat.alanwgt.com',
  axios
);
export const Workspaces = new ApiDeclaration.WorkspacesClient(
  'http://api.jchat.alanwgt.com',
  axios
);
