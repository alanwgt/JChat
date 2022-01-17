import React from 'react';
import { connect } from 'react-redux';
import { Display4, Paragraph3 } from 'baseui/typography';
import { useTranslation } from 'react-i18next';

import FullScreen from '@/components/display/FullScreen';
import WorkspaceList from '@/components/domain/workspace/WorkspaceList';
import { setWorkspace } from '@/store/workspace/workspace.actions';
import THEME from '@/styles/theme';
import { useStyletron } from 'baseui';

const WorkspaceSelector = ({ workspaces, selectWorkspace }) => {
  const [css] = useStyletron();
  const [t] = useTranslation();

  return (
    <FullScreen>
      <div
        className={css({
          padding: THEME.padding,
        })}
      >
        <Display4
          $style={{
            textAlign: 'center',
            marginBottom: THEME.padding,
          }}
        >
          {t('workspace.selector.title')}
        </Display4>
        <Paragraph3>{t('workspace.selector.description')}</Paragraph3>
      </div>
      <WorkspaceList
        workspaces={workspaces}
        onClick={({ id, name }) => {
          selectWorkspace(id, name);
        }}
      />
    </FullScreen>
  );
};

const mapDispatch = (dispatch) => ({
  selectWorkspace: (id, name) => dispatch(setWorkspace(id, name)),
});

export default connect(null, mapDispatch)(WorkspaceSelector);
