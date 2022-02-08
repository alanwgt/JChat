import React from 'react';

import { BaseProvider as BaseUiProvider } from 'baseui';
import { ToasterContainer } from 'baseui/toast';
import ReactDOM from 'react-dom';
import { Provider as ReactProvider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import { Client as Styletron } from 'styletron-engine-atomic';
import { DebugEngine, Provider as StyletronProvider } from 'styletron-react';

import App from '@/App';
import FullscreenSpinner from '@/components/feedback/FullscreenSpinner';
import { AuthProvider } from '@/providers/authContext';
import { PermissionsProvider } from '@/providers/permissionsContext';
import { UserProvider } from '@/providers/userContext';
import store from '@/store';
import '@/styles/global.css';
import '@/styles/lineIcon.css';
import '@/styles/reset.css';
import { baseUiTheme } from '@/styles/theme';

import './src/i18n';

const engine = new Styletron();
const debug =
  process.env.NODE_ENV === 'production' ? void 0 : new DebugEngine();

const render = () => {
  ReactDOM.render(
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
                  <PermissionsProvider>
                    <App />
                  </PermissionsProvider>
                </UserProvider>
              </AuthProvider>
            </ReactProvider>
          </BrowserRouter>
        </React.Suspense>
      </BaseUiProvider>
    </StyletronProvider>,
    document.getElementById('root')
  );
};

render();

if (module.hot) {
  module.hot.accept('./src/App', () => {
    render();
  });
}
