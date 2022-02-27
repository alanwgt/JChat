/* eslint no-param-reassign: 0 */
import produce from 'immer';

import { SetBootData } from '@/store/boot/boot.constants';
import {
  AddChannelMessage,
  AddChatChannel,
  RemoveChatChannel,
  SetChannelMessages,
  SetOnlineUsers,
  SetUserStatus,
} from '@/store/chat/chat.constants';

const initialState = {
  channels: [],
  userStatus: {},
  channelMessages: {},
};

const chatSlice = (state = initialState, action) => {
  switch (action.type) {
    case SetBootData:
      return {
        ...state,
        channels: action.payload.channels,
      };
    case AddChatChannel:
      return {
        ...state,
        channels: [action.payload.channel, ...state.channels],
      };
    case RemoveChatChannel:
      return produce(state, (draft) => {
        const idx = draft.channels.findIndex((c) => c.id === action.payload);

        if (idx !== -1) {
          draft.channels.splice(idx, 1);
        }
      });
    case SetUserStatus:
      return produce(state, (draft) => {
        draft.userStatus[action.payload.userId] = action.payload.status;
      });
    case SetChannelMessages:
      return produce(state, (draft) => {
        draft.channelMessages[action.payload.channelId] =
          action.payload.messages;
      });
    case AddChannelMessage:
      return produce(state, (draft) => {
        draft.channelMessages[action.payload.channelId].push(
          action.payload.message
        );
      });

    case SetOnlineUsers:
      return produce(state, (draft) => {
        draft.userStatus = {};
        action.payload.forEach((uId) => {
          draft.userStatus[uId] = true;
        });
      });
    default:
      return state;
  }
};

export default chatSlice;
