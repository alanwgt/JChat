import React from 'react';

import { Select } from 'baseui/select';
import { debounce } from 'lodash';
import { useTranslation } from 'react-i18next';

import { Users } from '@/api';
import Modal from '@/components/modal/Modal';
import feedbackUtils from '@/utils/feedback.utils';

const GlobalUserFinderModal = ({
  onSelect,
  scopeToWorkspace = false,
  ...props
}) => {
  const [t] = useTranslation();
  const [value, setValue] = React.useState(null);
  const [options, setOptions] = React.useState([]);
  const [isLoading, setIsLoading] = React.useState(false);

  const handleInputChange = debounce((query) => {
    if (!query || query.trim().length < 3) {
      setOptions([]);
      return;
    }

    setIsLoading(true);
    Users.query(query, scopeToWorkspace)
      .then(({ items }) => {
        setOptions(
          items.map((i) => ({
            label: i.username,
            ...i,
          }))
        );
      })
      .catch(feedbackUtils.error)
      .finally(() => setIsLoading(false));
  }, 500);

  return (
    <Modal
      title={t('users.global.finder.title')}
      onPositive={() => {
        onSelect(value);
      }}
      {...props}
    >
      <Select
        isLoading={isLoading}
        options={options}
        onChange={({ value }) => setValue(value)}
        onInputChange={(event) => {
          const { target } = event;
          handleInputChange(target.value);
        }}
        value={value}
        placeholder={t('users.global.finder.placeholder')}
      />
    </Modal>
  );
};

export default GlobalUserFinderModal;
