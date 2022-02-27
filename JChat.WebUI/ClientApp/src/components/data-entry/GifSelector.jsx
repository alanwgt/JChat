import React from 'react';

import { Grid, SearchContextManager } from '@giphy/react-components';
import { styled } from 'baseui';
import { Input, SIZE } from 'baseui/input';
import { debounce } from 'lodash';
import { useTranslation } from 'react-i18next';

import Backdrop from '@/components/display/Backdrop';
import { gf } from '@/services/gif.service';

const StyledContainer = styled('div', {
  width: '100vw',
  height: '75vh',
  position: 'absolute',
  display: 'flex',
  flexDirection: 'column',
  bottom: 0,
  left: 0,
});

const GifSelector = ({ onSelect }) => {
  const [search, setSearch] = React.useState('');
  const [dampenedSearch, setDampenedSearch] = React.useState('');
  const debouncedSetter = debounce((q) => {
    setDampenedSearch(q);
  }, 250);
  const [t] = useTranslation();

  const { innerWidth } = window;

  const fetchGifs = (offset) => {
    const options = { offset, limit: 9, type: 'gifs' };
    if (!search.trim().length) {
      return gf.trending(options);
    }

    return gf.search(search, { ...options, lang: 'en', sort: 'relevant' });
  };

  const setSearchValue = (q) => {
    setSearch(q);
    debouncedSetter(q);
  };

  return (
    <Backdrop>
      <SearchContextManager apiKey='haKiGXdMs9KnjKxe5DMTcjbYU2I5L4ZU'>
        <StyledContainer>
          <div>
            <Input
              clearable
              size={SIZE.compact}
              placeholder={t('gifs.search.placeholder')}
              value={search}
              onChange={({ currentTarget: { value } }) => {
                setSearchValue(value);
              }}
            />
          </div>
          <Grid
            noResultsMessage={t('gifs.search.no_result')}
            key={dampenedSearch}
            columns={3}
            fetchGifs={fetchGifs}
            width={innerWidth}
            onGifClick={(gif, e) => {
              e.preventDefault();
              onSelect(gif);
            }}
          />
        </StyledContainer>
      </SearchContextManager>
    </Backdrop>
  );
};

export default GifSelector;
