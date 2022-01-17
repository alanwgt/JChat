import { SetUser } from '@/store/user/user.constants';

export const setUserData = (user) => ({
  payload: user,
  type: SetUser,
});
