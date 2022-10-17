import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { AxiosError } from 'axios';

import { AppError } from '../../../../types';
import { DisplayPath, NotificationPayload, Preferences } from '../types';

type SharedState = {
  httpError?: AxiosError;
  initialized: boolean;
  initializationError?: {
    initializationState: string;
    abpError: AppError;
  };
  displayPath: DisplayPath | null | 'Skeleton';
  notification?: NotificationPayload;
  preferences: Preferences;
};

const initialState: SharedState = {
  initialized: false,
  preferences: {
    backend: {
      api: {},
    },
    version: 1,
  },
  displayPath: null,
};

const sharedSlice = createSlice({
  name: 'shared',
  initialState: initialState,
  reducers: {
    handleHttpErrorDefault: (state, action: PayloadAction<AxiosError>) => {
      state.httpError = action.payload;
    },
    setInitializationError: (state, action: PayloadAction<SharedState['initializationError']>) => {
      state.initializationError = action.payload;
    },
    initialized: (state) => {
      state.initialized = true;
    },
    showNotification: (state, action: PayloadAction<NotificationPayload>) => {
      state.notification = { ...action.payload, framework: action.payload.framework ?? 'Antd' };
    },
    hideNotification: (state) => {
      state.notification = undefined;
    },
    setPreferences: (state, action: PayloadAction<Preferences>) => {
      state.preferences = action.payload;
    },
    setDisplayPath: (state, action: PayloadAction<DisplayPath | null | 'Skeleton'>) => {
      if (action.payload && action.payload !== 'Skeleton') {
        action.payload.breadcrumbs = action.payload.breadcrumbs ?? [];
        if (!action.payload.disableAutoLastBreadcrumb) {
          action.payload.breadcrumbs.push({
            title: action.payload.title,
            path: !action.payload.disableAutoLastBreadcrumbLink ? action.payload.path : undefined,
          });
        }
      }

      state.displayPath = action.payload;
    },
  },
});

export default sharedSlice;
