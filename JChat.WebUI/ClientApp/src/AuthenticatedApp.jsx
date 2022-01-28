import React from 'react';
import { useStyletron } from 'baseui';

import Header from '@/components/layout/Header';
import Sidebar from '@/components/layout/Sidebar';
import FullScreen from '@/components/display/FullScreen';
import Routes from '@/routes/Routes';
import StarredMessages from '@/components/domain/chat/StarredMessages';
import { isStarredMessagesBarOpenSelector } from '@/store/ui/ui.selectors';
import { connect } from 'react-redux';

const AuthenticatedApp = ({ isStarredMessagesBarOpen }) => {
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
        {isStarredMessagesBarOpen && <StarredMessages />}
      </div>
    </FullScreen>
  );
};

const mapState = (state) => ({
  isStarredMessagesBarOpen: isStarredMessagesBarOpenSelector(state),
});

export default connect(mapState)(AuthenticatedApp);
