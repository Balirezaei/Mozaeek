import { AppListBaseResponse } from '../../mosaik';

export type AccountLoginRq = {
  username: string;
  password: string;
};

export type AccountLoginRs = {
  token: string;
  refreshToken: string;
};

export type AccountRefreshTokenRq = {
  token: string;
  refreshToken: string;
};
export type AccountRefreshTokenRs = AccountLoginRs;

export type UserRegisterRq = {
  userName: string;
  password: string;
  firstName: string;
  lastName: string;
  nationalCode: string;
  eMail: string;
  role: number;
};

export type UserUpdateRq = {
  userId: number;
  firstName: string;
  lastName: string;
  nationalCode: string;
  eMail: string;
  role: number;
};

export type UserItem = {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  nationalCode: string;
  eMail: string;
  roles: number[];
};

export type UserGetAllRs = AppListBaseResponse<UserItem>;

export type UserGetInitDtoRs = {
  roles: {
    text: string;
    value: string;
    selected: boolean;
  }[];
};
