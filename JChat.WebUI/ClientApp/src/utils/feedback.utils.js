import { PLACEMENT, toaster } from 'baseui/toast';

const toastConfig = {
  autoHideDuration: 3000,
  placement: PLACEMENT.topRight,
};

export default {
  positive: (children, props = {}) =>
    toaster.positive(children, {
      ...toastConfig,
      ...props,
    }),
  success: (children, props = {}) =>
    toaster.positive(children, {
      ...toastConfig,
      ...props,
    }),
  info: (children, props = {}) =>
    toaster.info(children, {
      ...toastConfig,
      ...props,
    }),
  warning: (children, props = {}) =>
    toaster.warning(children, {
      ...toastConfig,
      ...props,
    }),
  error: (children, props = {}) => {
    let message = children;

    if (children instanceof Error) {
      message = children.message;
    }

    toaster.negative(message, {
      ...toastConfig,
      ...props,
    });
  },
  negative: (children, props = {}) =>
    toaster.negative(children, {
      ...toastConfig,
      ...props,
    }),
};
