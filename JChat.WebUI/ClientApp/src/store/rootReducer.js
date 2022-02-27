import { combineReducers } from 'redux';

import bootSlice from '@/store/boot/boot.slice';
import chatSlice from '@/store/chat/chat.slice';
import uiSlice from '@/store/ui/ui.slice';
import userSlice from '@/store/user/user.slice';
import workspaceSlice from '@/store/workspace/workspace.slice';

export default combineReducers({
  boot: bootSlice,
  chat: chatSlice,
  ui: uiSlice,
  user: userSlice,
  workspace: workspaceSlice,
});
