import React from 'react';

import { useStyletron } from 'baseui';
import { HeadingMedium, ParagraphSmall } from 'baseui/typography';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';

import { Boot, Workspaces } from '@/api';
import AddResourceButton from '@/components/button/AddResourceButton';
import DownloadablePaginated from '@/components/data-display/DownloadablePaginated';
import FullscreenInput from '@/components/display/FullscreenInput';
import NotificationsModal from '@/components/domain/notifications/NotificationsModal';
import NotificationsToggle from '@/components/domain/notifications/NotificationsToggle';
import CreateWorkspaceModal from '@/components/domain/workspace/CreateWorkspaceModal';
import WorkspaceList from '@/components/domain/workspace/WorkspaceList';
import { setWorkspace } from '@/store/workspace/workspace.actions';
import feedbackUtils from '@/utils/feedback.utils';

const WorkspaceSelector = ({ selectWorkspace }) => {
  const [modalIsOpen, setModalIsOpen] = React.useState(false);
  const [workspacesList, setWorkspacesList] = React.useState([]);
  const [css] = useStyletron();
  const [t] = useTranslation();

  const onCreate = (workspace) => {
    setWorkspacesList([workspace, ...workspacesList]);
    setModalIsOpen(false);
    feedbackUtils.positive(t('workspaces.created.messsage'));
  };

  return (
    <DownloadablePaginated
      request={Workspaces.list.bind(Workspaces)}
      dataKey='workspaces'
      onResolve={({ items }) => {
        setWorkspacesList(items);
      }}
      render={() => (
        <>
          <CreateWorkspaceModal
            onCreate={onCreate}
            isOpen={modalIsOpen}
            setIsOpen={setModalIsOpen}
          />
          <NotificationsModal />
          <NotificationsToggle />
          <FullscreenInput>
            <div
              className={css({
                marginBottom: '20px',
              })}
            >
              <HeadingMedium
                $style={{
                  textAlign: 'center',
                  marginBottom: '20px',
                }}
              >
                {t('workspaces.selector.title')}
              </HeadingMedium>
              <div
                className={css({
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'space-between',
                })}
              >
                <ParagraphSmall>
                  {t('workspaces.selector.description')}
                </ParagraphSmall>
                <AddResourceButton
                  onClick={() => {
                    setModalIsOpen(true);
                  }}
                >
                  {t('workspaces.add.btn')}
                </AddResourceButton>
              </div>
            </div>
            <WorkspaceList
              workspaces={workspacesList}
              onClick={({ id, name }) => {
                selectWorkspace(id, name);
              }}
              className={css({
                width: '100%',
              })}
            />
          </FullscreenInput>
        </>
      )}
    />
  );
};

const mapDispatch = (dispatch) => ({
  selectWorkspace: (id, name) => dispatch(setWorkspace(id, name)),
});

export default connect(null, mapDispatch)(WorkspaceSelector);
