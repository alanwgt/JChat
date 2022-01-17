import React from 'react';
import { styled } from 'baseui';

import Header from '@/components/layout/Header';
import Sidebar from '@/components/layout/Sidebar';
import FullScreen from '@/components/display/FullScreen';
import Routes from '@/routes/Routes';
import StarredMessages from '@/components/domain/chat/StarredMessages';

const LowerSection = styled('div', {
  display: 'flex',
  height: 'calc(100vh - 38px)',
});

const AuthenticatedApp = () => (
  <FullScreen>
    <Header />
    <LowerSection>
      <Sidebar />
      <Routes />
      <StarredMessages />
    </LowerSection>
  </FullScreen>
);

export default AuthenticatedApp;
