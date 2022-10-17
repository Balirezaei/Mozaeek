import Axios from 'axios';

import { AxiosInstanceData } from '../../../../types';
import { AccountApiDataMap, AccountApiUrls } from './account-apiData';

const axiosInstance = Axios.create();
axiosInstance.defaults.baseURL = process.env.REACT_APP_AuthenticationApiBaseUrl;

export const changePasswordHttp = (data: { currentPassword: string; newPassword: string }) => {
  return axiosInstance.post(AccountApiUrls.ChangePassword, data);
};

export const updateProfileHttp = (data: {
  firstname: string;
  lastname: string;
  gender: boolean;
  birthDay?: Date;
  phoneNumber: string;
  timezone: string;
  nationalityId: string;
}) => {
  return axiosInstance.put(AccountApiUrls.UpdateProfile, data);
};

export const getCurrentProfileHttp = () => {
  return axiosInstance.get(AccountApiUrls.GetCurrentProfile);
};

export const changeLanguageHttp = (data: { languageName: string }) => {
  return axiosInstance.post(AccountApiUrls.ChangeLanguage, data);
};

export const AccountAxiosData: AxiosInstanceData = {
  instance: axiosInstance,
  DataMap: {
    MapObject: AccountApiDataMap,
    Urls: AccountApiUrls,
  },
};
