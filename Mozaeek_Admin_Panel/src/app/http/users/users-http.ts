import Axios from 'axios';

import { AxiosCustomRequestConfig, AxiosInstanceData, GetAllRequestBase } from '../../../types';
import { urlWithQuery } from '../../../utils/helpers';
import { UsersApiDataMap, UsersApiUrls } from './users-apiData';
import { AccountLoginRq, AccountRefreshTokenRq, UserRegisterRq, UserUpdateRq } from './usersApiTypes';

const axiosInstance = Axios.create();
axiosInstance.defaults.baseURL = process.env.REACT_APP_CoreApiBaseUrl;

//#region Users

export const userLoginHttp = (data: AccountLoginRq) => {
  return axiosInstance.post(UsersApiUrls.AccountLogin, data);
};

export const userRefreshTokenHttp = (data: AccountRefreshTokenRq) => {
  return axiosInstance.post(UsersApiUrls.AccountRefreshToken, data, { skipAuthRefresh: true } as AxiosCustomRequestConfig);
};

export const userAuthorizeHttp = () => {
  return axiosInstance.get(UsersApiUrls.AccountAuthorizedRequest, {
    disableRedirectOnUnauthorized: true,
  } as AxiosCustomRequestConfig);
};

//#endregion

//#region Users

export const userGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${UsersApiUrls.UserGetById}/${data.id}`);
};

export const userGetAllHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(UsersApiUrls.UserGetAll, data);
  return axiosInstance.get(url);
};

export const userRegisterHttp = (data: UserRegisterRq) => {
  return axiosInstance.post(UsersApiUrls.UserRegister, data);
};

export const userGetInitDtoHttp = () => {
  return axiosInstance.get(UsersApiUrls.UserGetInitDto);
};

export const userUpdateHttp = (data: UserUpdateRq) => {
  return axiosInstance.post(UsersApiUrls.UserUpdate, data);
};

export const userDeleteHttp = (data: { UserId: number }) => {
  const url = urlWithQuery(UsersApiUrls.UserDelete, data);
  return axiosInstance.get(url);
};

//#endregion

export const UsersAxiosData: AxiosInstanceData = {
  instance: axiosInstance,
  DataMap: {
    MapObject: UsersApiDataMap,
    Urls: UsersApiUrls,
  },
};
