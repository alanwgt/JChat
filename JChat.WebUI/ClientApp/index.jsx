import 'react-hot-loader';
import React from 'react';

import { BaseProvider as BaseUiProvider } from 'baseui';
import { ToasterContainer } from 'baseui/toast';
import { render } from 'react-dom';
import { Provider as ReactProvider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { Client as Styletron } from 'styletron-engine-atomic';
import { DebugEngine, Provider as StyletronProvider } from 'styletron-react';

import FullscreenSpinner from '@/components/feedback/FullscreenSpinner';
import { AuthProvider } from '@/providers/authContext';
import { UserProvider } from '@/providers/userContext';
import store from '@/store';
import '@/styles/global.css';
import '@/styles/lineIcon.css';
import '@/styles/reset.css';
import { baseUiTheme } from '@/styles/theme';

import App from './src/App';
import './src/i18n';

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
