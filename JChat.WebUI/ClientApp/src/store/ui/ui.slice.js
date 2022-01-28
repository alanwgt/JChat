import { ToggleStarred } from '@/store/ui/ui.constants';

const initialState = {
  isStarredMessagesBarOpen: false,
};

const uiSlice = (state = initialState, action) => {
  switch (action.type) {
    case ToggleStarred:
      return {
        ...state,
        isStarredMessagesBarOpen: !state.isStarredMessagesBarOpen,
      };
    default:
      return state;
  }
};

export default uiSlice;
