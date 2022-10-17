import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { AccountLoginRs } from '../../../http/users/usersApiTypes';

type AuthenticationState = {
  username?: string;
  loginTime?: number;
  isAuthenticated: boolean;
  authToken?: AccountLoginRs;
};

const initialState: AuthenticationState = {
  loginTime: undefined,
  isAuthenticated: false,
};

const authenticationSlice = createSlice({
  name: 'authentication',
  initialState: initialState,
  reducers: {
    setAuthToken: (state, action: PayloadAction<AccountLoginRs>) => {
      state.authToken = { token: action.payload.token, refreshToken: action.payload.refreshToken };
    },

    //just for testing
    clearAuthToken: (state) => {
      state.authToken = { refreshToken: state.authToken!.refreshToken, token: 'AAAAAAAAAA' };
    },

    login: (state, action: PayloadAction<{ username: string; role: string; date: number }>) => {
      state.username = action.payload.username;
      state.isAuthenticated = true;
      state.loginTime = action.payload.date;
    },
    logout: (state) => {
      state.isAuthenticated = false;
      state.loginTime = undefined;
      state.authToken = undefined;
    },
  },
});

export default authenticationSlice;
