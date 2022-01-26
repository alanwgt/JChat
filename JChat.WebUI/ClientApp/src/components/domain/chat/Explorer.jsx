import React from 'react';

import { styled } from 'baseui';
import { useTranslation } from 'react-i18next';

import DirectMessage from '@/components/domain/chat/DirectMessage';
import Downloadable from '@/components/data-display/Downloadable';
import { Channels } from '@/api';

const StyledPanel = styled('div', ({ $theme }) => ({
  padding: `0 5px ${$theme.padding} 5px`,
}));

const Explorer = ({ ...props }) => {
  const { t } = useTranslation();

  return (
    <div {...props}>
      <Downloadable
        request={Channels.list.bind(Channels)}
        render={({ data: { items } }) => (
          <StyledPanel header={t('explorer.channels')} key='1'>
            {items.map(({ id, name }) => (
              <DirectMessage key={id} name={name} />
            ))}
          </StyledPanel>
        )}
      />
      <StyledPanel header={t('explorer.direct')} key='2'></StyledPanel>
    </div>
  );
};

export default Explorer;
