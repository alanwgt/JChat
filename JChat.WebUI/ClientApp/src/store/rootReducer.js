import { combineReducers } from 'redux';

import channelsSlice from '@/store/channels/channels.slice';
import userSlice from '@/store/user/user.slice';
import workspaceSlice from '@/store/workspace/workspace.slice';
import uiSlice from '@/store/ui/ui.slice';

export default combineReducers({
  channels: channelsSlice,
  ui: uiSlice,
  user: userSlice,
  workspace: workspaceSlice,
});
