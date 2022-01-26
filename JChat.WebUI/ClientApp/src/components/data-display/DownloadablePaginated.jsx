import paginatedRemoteDataHoc from '@/hocs/paginatedRemoteDataHoc';

const DownloadablePaginated = ({ children }) => children;

export default paginatedRemoteDataHoc(DownloadablePaginated);
