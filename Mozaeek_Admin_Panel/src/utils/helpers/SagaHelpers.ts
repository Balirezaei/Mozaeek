import { ActionCreatorWithPayload, ActionCreatorWithoutPayload } from '@reduxjs/toolkit';
import { AxiosError, AxiosResponse } from 'axios';
import { call, put } from 'redux-saga/effects';

import sharedSlice from '../../app/modules/shared/redux/shared-slice';
import { ApiResponse, AppError, AppResponse } from '../../types';
import { handleAxiosError } from './AxiosHelpers';
import { getJsonStorageWithConditions, setJsonStorage } from './LocalStorageHelpers';

export function* appApi(
  httpPromise: (data: any) => Promise<ApiResponse<any>>,
  apiPayload: any,
  pendingAction: ActionCreatorWithoutPayload | undefined,
  errorAction: ActionCreatorWithPayload<AppError> | undefined,
  throwOnError = false
) {
  if (pendingAction) {
    yield put(pendingAction());
  }
  try {
    const axiosResponse: AxiosResponse = yield call(httpPromise, apiPayload);
    const error = handleAxiosError(axiosResponse);
    if (error) {
      if (errorAction) {
        yield put(errorAction(error));
      }
      const axiosError = axiosResponse as unknown as AxiosError;
      return { data: undefined, status: axiosError.response?.status };
    } else {
      return { data: axiosResponse.data, status: axiosResponse.status };
    }
  } catch (e) {
    if (throwOnError) {
      throw e;
    }
  }
}

export function* initializeFromLocalStorageAndApi<ApiResponseT>(
  localStorageKey: string,
  initializationState: string,
  conditionFn: (obj: ApiResponseT) => boolean,
  httpPromise: (data: any) => Promise<ApiResponse<ApiResponseT>>,
  apiPayload: any,
  onSuccess: (obj: { data: ApiResponseT; status: number; isCached: boolean }) => Generator | void,
  expireInSeconds: number,
  throwOnError: boolean
) {
  const storageObj = getJsonStorageWithConditions<ApiResponseT>(localStorageKey, conditionFn);
  if (storageObj) {
    yield onSuccess({ data: storageObj, status: 0, isCached: true });
    return storageObj;
  } else {
    const apiResponse: AxiosResponse<AppResponse<ApiResponseT>> = yield call(httpPromise, apiPayload);
    const error = handleAxiosError(apiResponse);
    if (error) {
      if (throwOnError) {
        yield put(sharedSlice.actions.setInitializationError({ initializationState, abpError: error }));
      }
      return undefined;
    } else {
      const result = apiResponse.data.data;
      yield onSuccess({ data: result, status: apiResponse.status, isCached: false });
      setJsonStorage(localStorageKey, result, expireInSeconds);
      return result;
    }
  }
}
