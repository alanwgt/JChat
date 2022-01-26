import React from 'react';
import { Route, Routes as RRDRoutes } from 'react-router-dom';
import Chat from '@/components/domain/chat/Chat';
import JChatPanel from '@/components/panels/JChatPanel';

const Routes = ({ ...props }) => (
  <RRDRoutes {...props}>
    <Route path='/channel/:channelId' element={<Chat />} />
    <Route path='*' element={<JChatPanel />} />
  </RRDRoutes>
);

export default Routes;
