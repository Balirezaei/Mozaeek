import { LocalStorageKey } from '../../../../features/constants';
import { store } from '../../../../features/redux';
import { fromJson, getStorage, setStorage, toJson } from '../../../../utils/helpers';
import { userRefreshTokenHttp } from '../../../http/users/users-http';
import { AccountRefreshTokenRs } from '../../../http/users/usersApiTypes';
import { authenticationSlice } from '../../authentication';

export const getAuthTokenStorage = () => {
  const authTokenStorage = getStorage(LocalStorageKey.AuthToken);
  if (authTokenStorage) {
    return fromJson(authTokenStorage) as AccountRefreshTokenRs;
  }
};

export const refreshAuthToken = (error?: any) => {
  const authToken = getAuthTokenStorage();

  if (authToken) {
    return userRefreshTokenHttp({ token: authToken.token, refreshToken: authToken.refreshToken }).then((response) => {
      if (response.status === 200 && response.data !== undefined) {
        const result = response.data.data as AccountRefreshTokenRs;
        store.dispatch(
          authenticationSlice.actions.setAuthToken({
            token: result.token,
            refreshToken: result.refreshToken,
          })
        );
        setStorage(LocalStorageKey.AuthToken, toJson(result)!);
      } else {
        return Promise.reject(error);
      }
    });
  } else {
    return Promise.reject(error);
  }
};
