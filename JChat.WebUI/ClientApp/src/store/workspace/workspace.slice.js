import {
  DESELECT_WORKSPACE,
  SET_WORKSPACE,
} from '@/store/workspace/workspace.constants';

const initialState = {
  id: null,
  name: null,
};

const workspaceSlice = (state = initialState, action) => {
  switch (action.type) {
    case SET_WORKSPACE:
      return {
        ...state,
        id: action.payload.id,
        name: action.payload.name,
      };
    case DESELECT_WORKSPACE:
      return initialState;
    default:
      return state;
  }
};

export default workspaceSlice;
