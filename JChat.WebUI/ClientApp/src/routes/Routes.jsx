import React from 'react';
import { Route, Routes as RRDRoutes } from 'react-router-dom';
import Chat from '@/components/domain/chat/Chat';

const Routes = ({ ...props }) => (
  <RRDRoutes {...props}>
    <Route path='*' element={<Chat />} />
  </RRDRoutes>
);

export default Routes;
