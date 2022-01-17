import { combineReducers } from 'redux';

import channelsSlice from '@/store/channels/channels.slice';
import userSlice from '@/store/user/user.slice';
import workspaceSlice from '@/store/workspace/workspace.slice';

export default combineReducers({
  channels: channelsSlice,
  user: userSlice,
  workspace: workspaceSlice,
});
