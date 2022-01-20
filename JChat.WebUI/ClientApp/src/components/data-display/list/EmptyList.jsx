import React from 'react';

import { styled } from 'baseui';
import { Paragraph4 } from 'baseui/typography';
import { useTranslation } from 'react-i18next';

import Icon from '@/components/typography/Icon';

const StyledContainer = styled('div', ({ $theme }) => ({
  width: '100%',
  height: '150px',
  fontSize: '52px',
  textAlign: 'center',
  backgroundColor: $theme.colors.neutralPrimaryAlpha,
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center',
  flexDirection: 'column',
  gap: '15px',
}));

const EmptyList = ({ message = null }) => {
  const [t] = useTranslation();

  return (
    <StyledContainer>
      <Icon name='sad-4' />
      <Paragraph4>{message || t('list.empty')}</Paragraph4>
    </StyledContainer>
  );
};

export default EmptyList;
