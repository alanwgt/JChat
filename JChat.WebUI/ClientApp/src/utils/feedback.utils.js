import React from 'react';

import { PLACEMENT, toaster } from 'baseui/toast';
import ReactDOM from 'react-dom';

import StylingEngine from '@/StylingEngine';
import ConfirmModal from '@/components/data-entry/ConfirmModal';
import i18n from '@/i18n';

const dataEl = document.getElementById('data');

const toastConfig = {
  autoHideDuration: 3000,
  placement: PLACEMENT.topRight,
};

export const confirm = (title, description) =>
  new Promise((resolve, reject) => {
    ReactDOM.render(
      <StylingEngine>
        <ConfirmModal
          title={title}
          description={description}
          onConfirm={resolve}
          onCancel={reject}
        />
      </StylingEngine>,
      dataEl
    );
  }).finally(() => {
    ReactDOM.unmountComponentAtNode(dataEl);
  });

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
      if (children.i18nKey) {
        message = i18n.t(children.i18nKey);
      } else if (children.title) {
        message = children.title;
      } else {
        message = children.message;
      }
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
