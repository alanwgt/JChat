import React from 'react';
import FullscreenSpinner from '@/components/feedback/FullscreenSpinner';
import Error from '@/components/display/Error';
import FullScreen from '@/components/display/FullScreen';

const waitForPromise =
  (WrappedComponent) =>
  ({ waitFor, ...props }) => {
    const [resolvedData, setResolvedData] = React.useState(null);
    const [error, setError] = React.useState(null);

    Promise.resolve(waitFor).then(setResolvedData).catch(setError);

    if (error !== null) {
      return (
        <FullScreen $center>
          <Error err={error} />
        </FullScreen>
      );
    }

    if (resolvedData === null) {
      return <FullscreenSpinner />;
    }

    return <WrappedComponent {...resolvedData} {...props} />;
  };

export default waitForPromise;
