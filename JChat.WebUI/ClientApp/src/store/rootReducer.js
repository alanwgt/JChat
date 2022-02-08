import { combineReducers } from 'redux';

import bootSlice from '@/store/boot/boot.slice';
import channelsSlice from '@/store/channels/channels.slice';
import uiSlice from '@/store/ui/ui.slice';
import userSlice from '@/store/user/user.slice';
import workspaceSlice from '@/store/workspace/workspace.slice';

export default combineReducers({
  channels: channelsSlice,
  ui: uiSlice,
  user: userSlice,
  workspace: workspaceSlice,
  boot: bootSlice,
});
