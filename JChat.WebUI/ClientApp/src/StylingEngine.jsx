import React from 'react';

import { BaseProvider as BaseUiProvider } from 'baseui';
import { Client as Styletron } from 'styletron-engine-atomic';
import { DebugEngine, Provider as StyletronProvider } from 'styletron-react';

import { baseUiTheme } from '@/styles/theme';

const engine = new Styletron();
const debug =
  process.env.NODE_ENV === 'production' ? void 0 : new DebugEngine();

const StylingEngine = ({ children }) => (
  <StyletronProvider value={engine} debug={debug} debugAfterHydration>
    <BaseUiProvider theme={baseUiTheme}>{children}</BaseUiProvider>
  </StyletronProvider>
);

export default StylingEngine;
