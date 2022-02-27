import React from 'react';

import ReactDOM from 'react-dom';

import StylingEngine from '@/StylingEngine';
import GifSelector from '@/components/data-entry/GifSelector';

const dataEl = document.getElementById('data');

export const selectGif = () => {
  let listener;

  return new Promise((resolve, reject) => {
    const onKeyDown = (event) => {
      if (event.key !== 'Escape') {
        return;
      }

      reject();
    };

    listener = onKeyDown;
    document.addEventListener('keydown', onKeyDown);

    ReactDOM.render(
      <StylingEngine>
        <GifSelector
          onSelect={(gif) => {
            resolve(gif);
          }}
        />
      </StylingEngine>,
      dataEl
    );
  }).finally(() => {
    document.removeEventListener('keydown', listener);
    ReactDOM.unmountComponentAtNode(dataEl);
  });
};
