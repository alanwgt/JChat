import React from 'react';

import { useStyletron } from 'baseui';
import { HeadingMedium, ParagraphSmall } from 'baseui/typography';
import { useTranslation } from 'react-i18next';
import { connect } from 'react-redux';

import AddResourceButton from '@/components/button/AddResourceButton';
import FullscreenInput from '@/components/display/FullscreenInput';
import CreateWorkspaceModal from '@/components/domain/workspace/CreateWorkspaceModal';
import WorkspaceList from '@/components/domain/workspace/WorkspaceList';
import { setWorkspace } from '@/store/workspace/workspace.actions';
import THEME from '@/styles/theme';
import feedbackUtils from '@/utils/feedback.utils';

const WorkspaceSelector = ({ workspaces, selectWorkspace }) => {
  const [modalIsOpen, setModalIsOpen] = React.useState(false);
  const [workspacesList, setWorkspacesList] = React.useState(workspaces);
  const [css] = useStyletron();
  const [t] = useTranslation();

  // TODO: add workspace invites

  const onCreate = (workspace) => {
    setWorkspacesList([...workspacesList, workspace]);
    setModalIsOpen(false);
    feedbackUtils.positive(t('workspace.created.messsage'));
  };

  return (
    <>
      <CreateWorkspaceModal
        onCreate={onCreate}
        isOpen={modalIsOpen}
        setIsOpen={setModalIsOpen}
      />
      <FullscreenInput>
        <div
          className={css({
            marginBottom: THEME.padding,
          })}
        >
          <HeadingMedium
            $style={{
              textAlign: 'center',
              marginBottom: THEME.padding,
            }}
          >
            {t('workspace.selector.title')}
          </HeadingMedium>
          <div
            className={css({
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'space-between',
            })}
          >
            <ParagraphSmall>
              {t('workspace.selector.description')}
            </ParagraphSmall>
            <AddResourceButton
              onClick={() => {
                setModalIsOpen(true);
              }}
            >
              {t('workspace.add.btn')}
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
  );
};

const mapDispatch = (dispatch) => ({
  selectWorkspace: (id, name) => dispatch(setWorkspace(id, name)),
});

export default connect(null, mapDispatch)(WorkspaceSelector);
