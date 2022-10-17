import React from 'react';

import { DefinedRoute } from '../../../types';

const prefixRoute = '/test';

const TestRoutes = (): readonly DefinedRoute[] => [
  { type: 'Route', exact: true, path: `${prefixRoute}/testc`, children: React.lazy(() => import('./views/TestC/TestC')) },
];

export default TestRoutes;
