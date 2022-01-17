import 'react-hot-loader';
import React from 'react';
import { render } from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { Provider as ReactProvider } from 'react-redux';
import { Client as Styletron } from 'styletron-engine-atomic';
import { DebugEngine, Provider as StyletronProvider } from 'styletron-react';
import { BaseProvider as BaseUiProvider } from 'baseui';
import { ToasterContainer } from 'baseui/toast';

import { AuthProvider } from '@/providers/authContext';
import { UserProvider } from '@/providers/userContext';

import App from './src/App';
import store from '@/store';

import { baseUiTheme } from '@/styles/theme';

import './src/i18n';
import '@/styles/reset.css';
import '@/styles/global.css';
import FullscreenSpinner from '@/components/feedback/FullscreenSpinner';

const engine = new Styletron();
const debug =
  process.env.NODE_ENV === 'production' ? void 0 : new DebugEngine();

render(
  <StyletronProvider value={engine} debug={debug} debugAfterHydration>
    <BaseUiProvider theme={baseUiTheme}>
      <React.Suspense
        fallback={<FullscreenSpinner message='Loading the shit' />}
      >
        <BrowserRouter>
          <ToasterContainer />
          <ReactProvider store={store}>
            <AuthProvider>
              <UserProvider>
                <App />
              </UserProvider>
            </AuthProvider>
          </ReactProvider>
        </BrowserRouter>
      </React.Suspense>
    </BaseUiProvider>
  </StyletronProvider>,
  document.getElementById('root')
);
