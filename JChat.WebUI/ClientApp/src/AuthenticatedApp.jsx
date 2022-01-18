import React from 'react';
import { useStyletron } from 'baseui';

import Header from '@/components/layout/Header';
import Sidebar from '@/components/layout/Sidebar';
import FullScreen from '@/components/display/FullScreen';
import Routes from '@/routes/Routes';
import StarredMessages from '@/components/domain/chat/StarredMessages';

const AuthenticatedApp = () => {
  const [css] = useStyletron();

  return (
    <FullScreen>
      <Header />
      <div
        className={css({
          display: 'flex',
          height: 'calc(100vh - 50px)',
        })}
      >
        <Sidebar />
        <Routes />
        <StarredMessages />
      </div>
    </FullScreen>
  );
};

export default AuthenticatedApp;
