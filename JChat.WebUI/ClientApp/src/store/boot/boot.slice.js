import { AddChannel, SetBootData } from '@/store/boot/boot.constants';

const initialState = {
  channels: [],
  me: {},
  messagePriorities: [],
  messageReactions: [],
  messageTypes: [],
  permissions: [],
  users: [],
};

const bootSlice = (state = initialState, action) => {
  switch (action.type) {
    case SetBootData:
      return {
        ...action.payload,
      };
    case AddChannel:
      return {
        ...state,
        channels: [action.payload, ...state.channels],
      };
    default:
      return state;
  }
};

export default bootSlice;
