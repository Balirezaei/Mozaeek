import { createAction } from '@reduxjs/toolkit';
import { all, call, put, takeLatest } from 'redux-saga/effects';

import { AppResponse } from '../../../../types';
import { appApi } from '../../../../utils/helpers';
import { getCurrentProfileHttp, updateProfileHttp } from '../http/account-http';
import { accountSlice } from '../index';
import { GetCurrentProfileResponse, UpdateProfileSagaPayload } from '../types';

const prefix = 'account_saga';

const UpdateProfile = `${prefix}/UpdateProfile`;
export const updateProfileSaga = createAction<UpdateProfileSagaPayload>(UpdateProfile);

const RefreshProfile = `${prefix}/RefreshProfile`;
export const refreshProfileSaga = createAction(RefreshProfile);

const SetCurrency = `${prefix}/SetCurrency`;
export const setCurrencySaga = createAction<string>(SetCurrency);

function* refreshProfile() {
  const response: AppResponse<GetCurrentProfileResponse> = yield call(
    appApi,
    getCurrentProfileHttp,
    undefined,
    accountSlice.actions.refreshProfilePending,
    accountSlice.actions.refreshProfileError
  );
  if (response && response.data) {
    yield put(accountSlice.actions.refreshProfileSuccess(response.data!));
  }
}

function* updateProfile(action: ReturnType<typeof updateProfileSaga>) {
  const updateProfileAxiosResponse: AppResponse = yield call(
    appApi,
    updateProfileHttp,
    action.payload,
    accountSlice.actions.updateProfilePending,
    accountSlice.actions.updateProfileError
  );
  if (updateProfileAxiosResponse) {
    const getProfileAxiosResponse: AppResponse<GetCurrentProfileResponse> = yield call(
      appApi,
      getCurrentProfileHttp,
      undefined,
      undefined,
      accountSlice.actions.updateProfileError
    );
    if (getProfileAxiosResponse) {
      yield put(accountSlice.actions.updateProfileSuccess(getProfileAxiosResponse.data!));
    }
  }
}

export function* watchAccount() {
  yield all([takeLatest(UpdateProfile, updateProfile), takeLatest(RefreshProfile, refreshProfile)]);
}
