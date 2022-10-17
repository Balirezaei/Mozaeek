import React from 'react';

import { DefinedRoute } from '../../../types';

const prefix = '/admin';

export const AdminPath = {
  Users: {
    Users: `${prefix}/users`,
    UserCreadit: `${prefix}/users/creadit`,
  },
};

const AdminRoutes: readonly DefinedRoute[] = [
  {
    type: 'Route',
    exact: true,
    path: `${prefix}/users`,
    children: React.lazy(() => import('./pages/Users/UserManagement')),
  },
  {
    type: 'Route',
    exact: true,
    path: `${prefix}/users/creadit`,
    children: React.lazy(() => import('./pages/Users/UserCreadit')),
  },
];

export default AdminRoutes;
