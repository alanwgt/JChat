import React from 'react';
import { Pagination, SIZE } from 'baseui/pagination';
import remoteDataHoc from '@/hocs/remoteDataHoc';

const paginatedRemoteDataHoc =
  (WrappedComponent) =>
  ({ request, params = {}, dataKey = 'data', render = null }) => {
    const [numPages, setNumPages] = React.useState(1);
    const [currPage, setCurrPage] = React.useState(1);

    const Component = React.useMemo(
      () => remoteDataHoc(WrappedComponent),
      [WrappedComponent]
    );

    return (
      <div>
        <Component
          request={request}
          params={{
            ...params,
            pageNumber: currPage,
            // TODO: size
          }}
          dataKey={dataKey}
          render={(props) => {
            const data = props[dataKey];
            // totalCount
            const { pageNumber, totalPages } = data;

            if (pageNumber !== currPage) setCurrPage(pageNumber);
            if (totalPages !== numPages) setNumPages(totalPages);

            if (render) {
              return render({ ...{ [dataKey]: data.items } });
            }

            return <WrappedComponent {...{ [dataKey]: data.items }} />;
          }}
        />
        {numPages > 1 && (
          <Pagination
            $style={{
              margin: '0 auto',
            }}
            size={SIZE.mini}
            numPages={numPages}
            currentPage={currPage}
            onPageChange={({ nextPage }) => {
              if (nextPage !== currPage) {
                setCurrPage(nextPage);
              }
            }}
          />
        )}
      </div>
    );
  };

export default paginatedRemoteDataHoc;
