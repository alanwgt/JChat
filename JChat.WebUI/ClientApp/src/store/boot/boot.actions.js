import { SetBootData } from '@/store/boot/boot.constants';

export const setBootData = (data) => ({
  type: SetBootData,
  payload: data,
});

export const addPermission = () => ({});
