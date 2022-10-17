import Axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios';

import { getAuthTokenStorage } from '../../app/modules/account/operations/authToken-operations';
import { ApiDataItem, ApiErrorStorage, AxiosCustomRequestConfig } from '../../types';
import { LocalizationHelpers, addOrSetArrayStorage, getNowISO, toJson } from '../../utils/helpers';
import { LocalStorageKey } from '../constants';

export const cancelRequest = (apiDataItem: ApiDataItem, message?: string) => {
  if (apiDataItem) {
    apiDataItem.cancelTokenSource?.cancel(message);
    apiDataItem.cancelTokenSource = undefined;
  }
};

export const requestSetAuthTokenInterceptor = (config: AxiosRequestConfig) => {
  const authToken = getAuthTokenStorage();

  config.headers = {
    'Content-Type': 'application/json',
    ...config.headers,
    'Accept-Language': LocalizationHelpers.getCurrentCulture().Name,
  };

  if (authToken) {
    config.headers['Authorization'] = `Bearer ${authToken.token}`;
  }
  return config;
};

export const requestSetCancelTokenInterceptor = async (config: AxiosRequestConfig) => {
  const apiDataItem = (config as AxiosCustomRequestConfig).apiDataItem;
  if (apiDataItem) {
    apiDataItem.cancelTokenSource = Axios.CancelToken.source();
    config.cancelToken = apiDataItem.cancelTokenSource.token;
  }
  return config;
};

export const responseSuccessNullifyCancelTokenInterceptor = async (response: AxiosResponse) => {
  const apiDataItem = (response.config as AxiosCustomRequestConfig).apiDataItem;
  if (apiDataItem) {
    apiDataItem!.cancelTokenSource = undefined;
  }
  return response;
};

export const responseErrorInterceptor = async (error: any) => {
  if (Axios.isCancel(error)) {
    console.info('Request cancelled');
    return Promise.resolve({ ___customCancel___: true });
  }

  const axiosError = error as AxiosError;

  const apiDataItem = (axiosError.config as AxiosCustomRequestConfig).apiDataItem;
  if (apiDataItem) {
    apiDataItem.cancelTokenSource = undefined;
  }

  addOrSetArrayStorage(
    LocalStorageKey.ApiErrors,
    { response: axiosError.response, message: axiosError.message, config: axiosError.config, date: getNowISO() } as ApiErrorStorage,
    200
  );

  // if (axiosError.response?.status === 401) {
  //   removeStorage(LocalStorageKey.AuthToken);
  //
  //   if (!(axiosError.config as AxiosCustomRequestConfig).disableRedirectOnUnauthorized) {
  //     window.location.href = '/auth/login';
  //   }
  // }

  if (axiosError.response) {
    console.error('Error Response', toJson(axiosError.response));
  }
  console.error('Error Message', axiosError.message);
  console.error('Error Request', axiosError.request);
  console.error('Error Config', toJson(axiosError.config));

  return Promise.resolve(axiosError);
};
