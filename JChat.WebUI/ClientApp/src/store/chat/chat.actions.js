import {
  AddChannelMessage,
  AddChannelMessageSlice,
  AddChatChannel,
  RemoveChatChannel,
  SetChannelMessages,
  SetOnlineUsers,
  SetUserStatus,
} from '@/store/chat/chat.constants';

export const addChatChannel = (channel, recentlyCreated = false) => ({
  type: AddChatChannel,
  payload: {
    channel,
    recentlyCreated,
  },
});

export const removeChatChannel = (channelId) => ({
  type: RemoveChatChannel,
  payload: channelId,
});

export const addChannelMessage = (channelId, message) => ({
  type: AddChannelMessage,
  payload: {
    channelId,
    message,
  },
});

export const addChannelMessageSlice = (channelId, slice) => ({
  type: AddChannelMessageSlice,
  payload: {
    channelId,
    slice,
  },
});

export const setUserStatus = (userId, status) => ({
  type: SetUserStatus,
  payload: {
    userId,
    status,
  },
});

export const setChannelMessages = (channelId, messages) => ({
  type: SetChannelMessages,
  payload: {
    channelId,
    messages,
  },
});

export const setOnlineUsers = (list) => ({
  type: SetOnlineUsers,
  payload: list,
});
