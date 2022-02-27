import React from 'react';

import { ToasterContainer } from 'baseui/toast';
import ReactDOM from 'react-dom';
import { Provider as ReactProvider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';

import App from '@/App';
import StylingEngine from '@/StylingEngine';
import FullscreenSpinner from '@/components/feedback/FullscreenSpinner';
import { AuthProvider } from '@/providers/authContext';
import { PermissionsProvider } from '@/providers/permissionsContext';
import { UserProvider } from '@/providers/userContext';
import store from '@/store';
import '@/styles/global.css';
import '@/styles/lineIcon.css';
import '@/styles/reset.css';

import './src/i18n';

const render = () => {
  ReactDOM.render(
    <StylingEngine>
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
    </StylingEngine>,
    document.getElementById('root')
  );
};

render();

if (module.hot) {
  module.hot.accept('./src/App', () => {
    render();
  });
}
