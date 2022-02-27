import produce from 'immer';

import { SetBootData } from '@/store/boot/boot.constants';
import { AddChatChannel } from '@/store/chat/chat.constants';

const initialState = {
  me: {},
  messagePriorities: [],
  messageReactions: [],
  messageTypes: [],
  permissions: [],
  users: [],
};

const makePermission = (ns, objId, rel) => ({
  namespace: ns,
  object: objId,
  relation: rel,
});

const bootSlice = (state = initialState, action) => {
  switch (action.type) {
    case SetBootData:
      return {
        ...action.payload,
      };
    case AddChatChannel:
      if (action.payload.recentlyCreated) {
        return produce(state, (draft) => {
          draft.permissions.push(
            makePermission('channels', action.payload.channel.id, 'ownership')
          );
        });
      }

      return state;
    default:
      return state;
  }
};

export default bootSlice;
