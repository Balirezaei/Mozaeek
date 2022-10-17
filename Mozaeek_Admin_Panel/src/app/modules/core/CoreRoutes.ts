import React from 'react';

import { DefinedRoute } from '../../../types';

export const CorePaths = {
  Rss: '/announcement/rss',
  RssCreadit: '/announcement/rss/creadit',
  News: '/announcement/news',
  NewsCreadit: '/announcement/news/creadit',
  Labels: '/basic/labels',
  RequestAct: '/basic/requestact',
  Points: '/basic/points',
  RequestOrganizations: '/basic/requestorganizations',
  Subjects: '/basic/subjects',
  RequestTargets: '/requests/requesttargets',
  RequestTargetCreadit: '/requests/requesttarget/creadit',
  Requests: '/requests/requests',
  RequestCreadit: '/requests/requests/creadit',
  PreRequest: '/requests/prerequests',
  SubjectPricing: {
    List: '/pricing/subjects',
    Creadit: '/pricing/subjects/creadit',
  },
  RequestPricing: {
    List: '/pricing/requests',
    Creadit: '/pricing/requests/creadit',
  },
};

const CoreRoutes: readonly DefinedRoute[] = [
  { type: 'Route', path: CorePaths.Rss, exact: true, children: React.lazy(() => import('./pages/RSS/RSS')) },
  { type: 'Route', path: CorePaths.RssCreadit, exact: true, children: React.lazy(() => import('./pages/RSS/RssCreadit')) },
  { type: 'Route', path: CorePaths.News, exact: true, children: React.lazy(() => import('./pages/News/News')) },
  { type: 'Route', path: CorePaths.NewsCreadit, exact: true, children: React.lazy(() => import('./pages/News/NewsCreadit')) },
  { type: 'Route', path: CorePaths.Labels, exact: true, children: React.lazy(() => import('./pages/Labels/Labels')) },
  { type: 'Route', path: CorePaths.RequestAct, exact: true, children: React.lazy(() => import('./pages/RequestActs/RequestAct')) },
  { type: 'Route', path: CorePaths.Points, exact: true, children: React.lazy(() => import('./pages/Points/Points')) },
  {
    type: 'Route',
    path: CorePaths.RequestOrganizations,
    exact: true,
    children: React.lazy(() => import('./pages/RequestOrganizations/RequestOrganizations')),
  },
  { type: 'Route', path: CorePaths.Subjects, exact: true, children: React.lazy(() => import('./pages/Subjects/Subjects')) },
  { type: 'Route', path: CorePaths.RequestTargets, exact: true, children: React.lazy(() => import('./pages/RequestTargets/RequestTargets')) },
  {
    type: 'Route',
    path: CorePaths.RequestTargetCreadit,
    exact: true,
    children: React.lazy(() => import('./pages/RequestTargets/RequestTargetCreadit')),
  },
  { type: 'Route', path: CorePaths.Requests, exact: true, children: React.lazy(() => import('./pages/Requests/Requests')) },
  { type: 'Route', path: CorePaths.RequestCreadit, exact: true, children: React.lazy(() => import('./pages/Requests/RequestCreadit')) },
  { type: 'Route', path: CorePaths.PreRequest, exact: true, children: React.lazy(() => import('./pages/PreRequest/PreRequests')) },
  { type: 'Route', path: CorePaths.SubjectPricing.List, exact: true, children: React.lazy(() => import('./pages/Pricing/SubjectsPricing')) },
  { type: 'Route', path: CorePaths.SubjectPricing.Creadit, exact: true, children: React.lazy(() => import('./pages/Pricing/SubjectPricingCreadit')) },
  { type: 'Route', path: CorePaths.RequestPricing.List, exact: true, children: React.lazy(() => import('./pages/Pricing/RequestsPricing')) },
  { type: 'Route', path: CorePaths.RequestPricing.Creadit, exact: true, children: React.lazy(() => import('./pages/Pricing/RequestPricingCreadit')) },
];

export default CoreRoutes;
