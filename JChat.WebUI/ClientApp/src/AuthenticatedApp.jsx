import React from 'react';

import { useStyletron } from 'baseui';
import { useTranslation } from 'react-i18next';
import { connect, useDispatch } from 'react-redux';

import { Boot } from '@/api';
import FullScreen from '@/components/display/FullScreen';
import StarredMessages from '@/components/domain/chat/StarredMessages';
import FullscreenSpinner from '@/components/feedback/FullscreenSpinner';
import Header from '@/components/layout/Header';
import Sidebar from '@/components/layout/Sidebar';
import Routes from '@/routes/Routes';
import {
  connectToWorkspace,
  disconnectFromWorkspace,
} from '@/services/signalr.service';
import { setBootData } from '@/store/boot/boot.actions';
import { isStarredMessagesBarOpenSelector } from '@/store/ui/ui.selectors';
import { deselectWorkspace } from '@/store/workspace/workspace.actions';
import { workspaceSelector } from '@/store/workspace/workspace.selectors';
import feedbackUtils from '@/utils/feedback.utils';

const AuthenticatedApp = ({ workspace, isStarredMessagesBarOpen }) => {
  const [booted, setbooted] = React.useState(false);
  const dispatch = useDispatch();
  const [t] = useTranslation();
  const [css] = useStyletron();

  React.useEffect(() => {
    connectToWorkspace(workspace.id);

    Boot.init()
      .then((data) => {
        dispatch(setBootData(data));
        setbooted(true);
      })
      .catch((err) => {
        const errStatus = err.response?.status || 500;

        if (errStatus === 401 || errStatus === 403) {
          dispatch(deselectWorkspace());
        } else {
          feedbackUtils.error(err);
        }
      });

    return () => {
      disconnectFromWorkspace(workspace.id);
    };
  }, []);

  if (!booted) {
    return <FullscreenSpinner message={t('workspaces.loading')} />;
  }

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
  workspace: workspaceSelector(state),
  isStarredMessagesBarOpen: isStarredMessagesBarOpenSelector(state),
});

export default connect(mapState)(AuthenticatedApp);
