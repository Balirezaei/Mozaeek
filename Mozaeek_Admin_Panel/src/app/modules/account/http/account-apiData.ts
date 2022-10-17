import { ApiDataItem } from '../../../../types';

export enum AccountApiUrls {
  ChangePassword = '/api/services/app/Account/ChangePassword',
  UpdateProfile = '/api/services/app/Profile/UpdateCurrentUserProfile',
  GetCurrentProfile = '/api/services/app/Profile/GetCurrentUserProfileForEdit',
  ChangeLanguage = '/api/services/app/Profile/ChangeLanguage',
}

export const AccountApiDataMap: Map<string, ApiDataItem> = new Map();
