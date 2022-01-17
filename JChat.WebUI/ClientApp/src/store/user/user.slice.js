import { SetUser } from '@/store/user/user.constants';

const initialState = {
  id: null,
  username: null,
  firstname: null,
  lastname: null,
};

const userSlice = (state = initialState, action) => {
  switch (action.type) {
    case SetUser:
      return {
        ...action.payload,
      };
    default:
      return state;
  }
};

export default userSlice;
