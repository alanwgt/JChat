import React from 'react';

import { useStyletron } from 'baseui';
import { StyledSpinnerNext } from 'baseui/spinner';
import _ from 'lodash';

import Error from '@/components/display/Error';

const remoteDataHoc =
  (WrappedComponent) =>
  ({
    request,
    params = {},
    dataKey = 'data',
    render = null,
    onResolve = null,
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
      Promise.resolve(request(params))
        .then((responseData) => {
          setData(responseData);
          onResolve?.(responseData);
        })
        .catch(setError);
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
