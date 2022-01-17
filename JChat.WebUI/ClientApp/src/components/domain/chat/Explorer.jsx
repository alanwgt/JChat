import React from 'react';
import { useTranslation } from 'react-i18next';
import { styled } from 'baseui';

import DirectMessage from '@/components/domain/chat/DirectMessage';
import THEME from '@/styles/theme';

const StyledPanel = styled('div', {
  padding: `0 5px ${THEME.padding} 5px`,
});

const Explorer = ({ ...props }) => {
  const { t } = useTranslation();

  return (
    <div {...props}>
      <StyledPanel header={t('explorer.channels')} key='1'></StyledPanel>
      <StyledPanel header={t('explorer.direct')} key='2'>
        <DirectMessage name='Alan' />
        <DirectMessage name='Carlo' />
        <DirectMessage name='Diogo' />
        <DirectMessage name='Vic' />
        <DirectMessage name='NOME COMAOSD JHASDJIOASJIOD JOIASDIJO SAHDIUSAHDHAS' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
        <DirectMessage name='Vic' />
      </StyledPanel>
    </div>
  );
};

export default Explorer;
