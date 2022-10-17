import React from 'react';

export type NotificationPayload = {
  framework?: 'MaterialUi' | 'Antd';
  content: string;
  type: 'success' | 'info' | 'warning' | 'error';
  longer?: boolean;
};

export type BreadcrumbType = { title: string; path?: string; tooltip?: React.ReactNode };

export type DisplayPath = {
  fontawesomeIcon?: string;
  title: string;
  breadcrumbs?: BreadcrumbType[];
  path?: string;
  disableAutoLastBreadcrumb?: true;
  disableAutoLastBreadcrumbLink?: true;
};

export type JwtTokenClaims = {
  Username: string;
  UserData: string;
  UserId: string;
  UserRole: string;
  NotValidBefore: string;
  ExpirationTime: string;
  Issuer: string;
  Audience: string;
};

export type Preferences = {
  backend: {
    api: {
      [key: string]: {
        table?: {
          pagination: {
            pageSize: number;
            pageNumber: number;
          };
        };
      };
    };
  };
  version: number;
};
