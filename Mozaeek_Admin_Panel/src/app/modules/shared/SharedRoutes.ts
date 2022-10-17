import React from 'react';

import { DefinedRoute } from '../../../types';

const SharedRoutes = (): readonly DefinedRoute[] => [
  { type: 'Route', exact: true, path: '/home', children: React.lazy(() => import('./pages/Home/Home')) },
  { type: 'Route', exact: true, path: '/unauthorized', children: React.lazy(() => import('./pages/Unauthorized401/Unauthorized401')) },
  { type: 'Route', exact: true, path: '/notfound', children: React.lazy(() => import('./pages/NotFound404/NotFound404')) },
  { type: 'Route', exact: true, path: '/internalservererror', children: React.lazy(() => import('./pages/InternalServerError500/InternalServerError500')) },
];

export default SharedRoutes;
