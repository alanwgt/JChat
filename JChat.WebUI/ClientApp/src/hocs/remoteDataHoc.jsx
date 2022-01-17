import React from 'react';
import { StyledSpinnerNext } from 'baseui/spinner';

import Error from '@/components/display/Error';

const remoteDataHoc =
  (WrappedComponent) =>
  ({ request, params = {}, dataKey = 'data', render = null }) => {
    const [data, setData] = React.useState(null);
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
      Promise.resolve(request(params)).then(setData).catch(setError);
    }, [params]);

    if (error) {
      return <Error err={error} />;
    }

    if (!data) {
      return <StyledSpinnerNext size={100} />;
    }

    const props = { [dataKey]: data };

    if (render) {
      return render(props);
    }

    return <WrappedComponent {...props} />;
  };

export default remoteDataHoc;
