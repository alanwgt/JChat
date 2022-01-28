import React from 'react';
import _ from 'lodash';
import { StyledSpinnerNext } from 'baseui/spinner';

import Error from '@/components/display/Error';
import { useStyletron } from 'baseui';

const remoteDataHoc =
  (WrappedComponent) =>
  ({
    request,
    params = {},
    dataKey = 'data',
    render = null,
    ...extraProps
  }) => {
    const [css] = useStyletron();
    const [data, setData] = React.useState(null);
    const [error, setError] = React.useState(null);
    const [lastRequestParams, setLastRequestParams] = React.useState(null);

    React.useEffect(() => {
      if (_.isEqual(lastRequestParams, params)) {
        return;
      }

      setLastRequestParams(params);
      Promise.resolve(request(params)).then(setData).catch(setError);
    }, [params]);

    if (error) {
      return <Error err={error} />;
    }

    if (!data) {
      return (
        <div
          className={css({
            width: '100%',
            display: 'flex',
            justifyContent: 'center',
          })}
        >
          <StyledSpinnerNext size={100} />
        </div>
      );
    }

    const props = { ...extraProps, [dataKey]: data };

    if (render) {
      return render(props);
    }

    return <WrappedComponent {...props} />;
  };

export default remoteDataHoc;
