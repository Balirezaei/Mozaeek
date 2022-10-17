import React from 'react';
import { Switch } from 'react-router-dom';

import { Layout } from '../../_metronic/layout';
import { useAppSelector } from '../../features/hooks';
import { DefinedRoute } from '../../types';
import { createFromDefinedRoutes } from '../../utils/helpers';
import { AccountRoutes } from './account';
import { AdminRoutes } from './admin';
import { AuthRoutes, LogoutPage } from './authentication';
import { CoreRoutes } from './core';
import { SharedRoutes } from './shared';
import { TestRoutes } from './testModule';

const Routes = React.memo(() => {
  const isAuthenticated = useAppSelector((state) => state.authentication.isAuthenticated);

  let mainRoutes: DefinedRoute[] | undefined = undefined;

  if (isAuthenticated) {
    mainRoutes = [
      { type: 'Route', path: '/auth/logout', children: LogoutPage, exact: true },
      { type: 'Builder', children: createFromDefinedRoutes(SharedRoutes()) },
      { type: 'Builder', children: createFromDefinedRoutes(AccountRoutes()) },
      { type: 'Builder', children: createFromDefinedRoutes(AdminRoutes) },
      { type: 'Builder', children: createFromDefinedRoutes(CoreRoutes) },
      { type: 'Builder', children: createFromDefinedRoutes(TestRoutes()) },
      { type: 'Redirect', exact: true, from: '/', path: '/home' },
      { type: 'Redirect', path: '/notfound' },
    ];
  }

  return mainRoutes ? (
    <Layout>
      <Switch>{createFromDefinedRoutes(mainRoutes)}</Switch>
    </Layout>
  ) : (
    <Switch>{createFromDefinedRoutes(AuthRoutes)}</Switch>
  );
});

export default Routes;
