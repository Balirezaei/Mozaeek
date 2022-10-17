import { ApiDataItem } from '../../../types';

export enum UsersApiUrls {
  //Account
  AccountLogin = '/api/account/Login',
  AccountRefreshToken = '/api/account/RefreshToken',
  AccountAuthorizedRequest = '/api/account/AuthorizedRequest',

  //User
  UserGetById = '/api/User/GetById',
  UserGetAll = '/api/User/GetAll',
  UserRegister = '/api/User/Register',
  UserGetInitDto = '/api/User/GetUserInitDto',
  UserUpdate = '/api/User/Update',
  UserDelete = '/api/User/Delete',
}

export const UsersApiDataMap: Map<string, ApiDataItem> = new Map();
