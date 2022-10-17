import React from 'react';

import { DefinedRoute } from '../../../types';

const prefix = '/account';

const AccountRoutes = (): readonly DefinedRoute[] => [
  {
    type: 'Route',
    path: `${prefix}/profile`,
    children: React.lazy(() => import('./pages/Profile/Profile')),
  },
];

export default AccountRoutes;
