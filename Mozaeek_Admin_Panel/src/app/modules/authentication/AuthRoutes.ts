import React from 'react';

import { DefinedRoute } from '../../../types';

const AuthRoutes: DefinedRoute[] = [
  { type: 'Route', path: '/auth', children: React.lazy(() => import('./pages/AuthPage/AuthPage')) },
  { type: 'Redirect', path: '/auth' },
];

export default AuthRoutes;
