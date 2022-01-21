import React from 'react';

import { styled } from 'baseui';
import { useTranslation } from 'react-i18next';

import DirectMessage from '@/components/domain/chat/DirectMessage';

const StyledPanel = styled('div', ({ $theme }) => ({
  padding: `0 5px ${$theme.padding} 5px`,
}));

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
