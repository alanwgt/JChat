import {
  DESELECT_WORKSPACE,
  SET_WORKSPACE,
} from '@/store/workspace/workspace.constants';

export const setWorkspace = (id, name) => ({
  payload: {
    id,
    name,
  },
  type: SET_WORKSPACE,
});

export const deselectWorkspace = () => ({
  type: DESELECT_WORKSPACE,
});
