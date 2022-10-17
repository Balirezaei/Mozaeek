import createAuthRefreshInterceptor from 'axios-auth-refresh';

import { refreshAuthToken } from '../../app/modules/account';
import { AxiosCustomRequestConfig, AxiosInstanceData } from '../../types';
import { getApiUrlPart, getStorage } from '../../utils/helpers';
import { LocalStorageKey } from '../constants';
import {
  requestSetAuthTokenInterceptor,
  requestSetCancelTokenInterceptor,
  responseErrorInterceptor,
  responseSuccessNullifyCancelTokenInterceptor,
} from './configAxios';

export const setupAxiosInstances = (axiosInstancesData: AxiosInstanceData[]) => {
  const language = getStorage(LocalStorageKey.Culture);

  for (const item of axiosInstancesData) {
    item.instance.interceptors.request.use(requestSetAuthTokenInterceptor);
    item.instance.interceptors.request.use(requestSetCancelTokenInterceptor);
    item.instance.interceptors.request.use((config) => {
      const axiosConfig = config as AxiosCustomRequestConfig;
      if (axiosConfig.url) {
        const urlKey = getApiUrlPart(axiosConfig.url);

        axiosConfig.apiDataItem = item.DataMap.MapObject.get(urlKey);
      }
      return axiosConfig;
    });

    createAuthRefreshInterceptor(
      item.instance,
      (error: any) => {
        return refreshAuthToken(error);
      },
      {
        pauseInstanceWhileRefreshing: true,
      }
    );
    item.instance.interceptors.response.use(responseSuccessNullifyCancelTokenInterceptor, responseErrorInterceptor);
    item.instance.defaults.headers['Accept-Language'] = language;

    //fill api data map
    for (const key of Object.values(item.DataMap.Urls)) {
      item.DataMap.MapObject.set(key, {});
    }
  }
};
