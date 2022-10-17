import { createAction } from '@reduxjs/toolkit';
import { AxiosError } from 'axios';
import { History } from 'history';
import { all, call, put, takeLatest } from 'redux-saga/effects';

import { LocalStorageKey } from '../../../../features/constants';
import { SagaAppApiResponse } from '../../../../types';
import { appApi, getJsonStorage, removeStorage } from '../../../../utils/helpers';
import { userAuthorizeHttp } from '../../../http/users/users-http';
import { AccountLoginRs } from '../../../http/users/usersApiTypes';
import { accountSlice } from '../../account';
import { authenticationSlice } from '../../authentication';
import { getPreferences } from '../helpers';
import { decodeAppJwt } from '../helpers/AppJwtHelpers';
import sharedSlice from './shared-slice';

//#region actions

const prefix = 'shared_saga';

const HandleHttpError = `${prefix}/HandleHttpError`;
export const handleHttpErrorDefaultSaga = createAction<AxiosError>(HandleHttpError);

const InitializeApp = `${prefix}/InitializeApp`;
export const initializeAppSaga = createAction<{ history: History }>(InitializeApp);

// const GetIPLocation = `${prefix}/GetIPLocation`;
// export const getIPLocationSaga = createAction(GetIPLocation);

//#endregion

function* handleHttpErrorDefault(action: ReturnType<typeof handleHttpErrorDefaultSaga>) {
  const payload = action.payload;
  yield put(sharedSlice.actions.handleHttpErrorDefault(payload));
}

function* initializeApp() {
  try {
    //let error;

    //#region Authentication Token
    const authToken = getJsonStorage(LocalStorageKey.AuthToken) as AccountLoginRs;
    if (authToken) {
      yield put(authenticationSlice.actions.setAuthToken(authToken));

      const authorized: SagaAppApiResponse<string> = yield call(appApi, userAuthorizeHttp, undefined, undefined, undefined, false);
      if (authorized.status === 200) {
        const jwt = decodeAppJwt(authToken.token);
        if (jwt) {
          yield put(authenticationSlice.actions.login({ username: jwt.Username, role: jwt.UserRole, date: Date.now() }));
        }
      }
    }
    //#endregion

    //#region Preferences

    const preferences = getPreferences();
    yield put(sharedSlice.actions.setPreferences(preferences));

    //#endregion

    //#region Get User And Permissions
    // if (authToken) {
    //   const profileAxiosResponse: ApiResponse<GetCurrentProfileResponse> = yield call(getCurrentProfileHttp);
    //   error = handleAxiosError(profileAxiosResponse);
    //   if (error) {
    //     yield put(sharedSlice.actions.setInitializationError({ initializationState: 'Getting user information', abpError: error }));
    //
    //     //Test
    //     //yield put(sharedSlice.actions.setInitializationError({ initializationState: 'Getting user information', abpError: { message: 'AAAA', code: 0 } }));
    //
    //     return;
    //   } else {
    //     const {
    //       data: { data: profile },
    //     } = profileAxiosResponse as AxiosResponse<AppResponse<GetCurrentProfileResponse>>;
    //     yield put(accountSlice.actions.updateProfileSuccess(profile!));
    //     yield put(accountSlice.actions.updateProfileReset());
    //   }
    // }

    //#endregion

    yield put(sharedSlice.actions.initialized());
  } catch (ex: any) {
    console.error('Initialization', ex);

    removeStorage(LocalStorageKey.AuthToken);

    yield put(accountSlice.actions.removeProfile());
    yield put(authenticationSlice.actions.logout());

    if (!window.location.href.includes('/auth/login')) {
      window.location.href = '/auth/login';
    } else {
      localStorage.setItem('__CE__', ex.toString()!);
      window.location.reload();
    }
  }
}

// export function* getIPLocation() {
//   const ipLocationResponse: AbpResponse<GetIpLocationResponse> = yield abpApi(getIPLocationHttp, undefined, undefined, undefined);
//   if (ipLocationResponse) {
//     yield put(sharedSlice.actions.setIpLocation(ipLocationResponse.result!));
//   }
// }

export function* watchShared() {
  yield all([takeLatest(HandleHttpError, handleHttpErrorDefault), takeLatest(InitializeApp, initializeApp)]);
}
//, takeLatest(GetIPLocation, getIPLocation)
