import React from 'react';
import { styled } from 'baseui';
import HackedText from '@/components/typography/HackedText';
import Logo from '@/components/typography/icons/Logo';
import { DisplayMedium, ParagraphSmall } from 'baseui/typography';

const StyledContainer = styled('div', ({ $theme }) => ({
  display: 'flex',
  width: '100%',
  height: '100%',
  justifyContent: 'center',
  alignItems: 'center',
  flexDirection: 'column',
  backgroundColor: $theme.colors.b2,
  padding: '25px',
}));

const StyledTitleContainer = styled('div', {
  display: 'flex',
  alignItems: 'center',
  gap: '15px',
  marginBottom: '20px',
});

const StyledParagraph = styled(ParagraphSmall, ({ $theme }) => ({
  color: $theme.colors.neutralSecondary,
  ':not(:last-child)': {
    marginBottom: '15px',
  },
  maxWidth: '500px',
}));

const JChatPanel = () => (
  <StyledContainer>
    <StyledTitleContainer>
      <Logo width='24px' />
      <DisplayMedium>
        <HackedText text='JChat'>JChat</HackedText>
      </DisplayMedium>
    </StyledTitleContainer>
    <StyledParagraph>
      Lorem ipsum dolor sit amet, consectetur adipisicing elit. Alias aspernatur
      deleniti maiores nobis odit quasi quibusdam quidem! Cum excepturi impedit
      iure labore officiis pariatur provident, tempora! Et ipsam provident
      saepe!
    </StyledParagraph>
    <StyledParagraph>
      Accusantium delectus ex expedita laborum natus optio porro quisquam rem
      repudiandae sed! Asperiores consequuntur delectus, dignissimos dolore
      dolores ducimus, earum id in incidunt laborum, libero molestiae neque
      officia totam voluptates?
    </StyledParagraph>
  </StyledContainer>
);

export default JChatPanel;
