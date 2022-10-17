import { PayloadAction, createSlice } from '@reduxjs/toolkit';

import { LocalStorageKey } from '../../../../features/constants';
import { CultureName, SupportedCulture } from '../../../../features/localization/cultures';
import { ApiReduxState, AppError } from '../../../../types';
import { LocalizationHelpers, getStorage, resetApiState, setApiErrorState, setApiPendingState, setApiSuccessState } from '../../../../utils/helpers';
import { GetCurrentProfileResponse } from '../types';

type AccountState = {
  profile?: GetCurrentProfileResponse;
  culture: SupportedCulture;
  updateProfileApi: ApiReduxState;
  refreshProfileApi: ApiReduxState;
};

const initialState: AccountState = {
  profile: {
    userId: 1,
    firstname: 'Demo',
    lastname: 'Demo',
    isActive: true,
    userName: 'Demo@Demo.ir',
  },
  culture: LocalizationHelpers.getCultureFromCultureName(getStorage(LocalStorageKey.Culture) as CultureName) ?? LocalizationHelpers.defaultCulture,
  updateProfileApi: {},
  refreshProfileApi: {},
};

const accountSlice = createSlice({
  name: 'account',
  initialState: initialState,
  reducers: {
    refreshProfilePending: (state) => {
      setApiPendingState(state.refreshProfileApi);
    },
    refreshProfileSuccess: (state, action: PayloadAction<GetCurrentProfileResponse>) => {
      setApiSuccessState(state.refreshProfileApi);

      state.profile = action.payload;
    },
    refreshProfileError: (state, action: PayloadAction<AppError>) => {
      setApiErrorState(state.refreshProfileApi, action.payload);
    },
    updateProfilePending: (state) => {
      setApiPendingState(state.updateProfileApi);
    },
    updateProfileSuccess: (state, action: PayloadAction<GetCurrentProfileResponse>) => {
      setApiSuccessState(state.updateProfileApi);

      state.profile = { ...action.payload, phoneNumber: action.payload.phoneNumber ?? undefined };
    },
    updateProfileError: (state, action: PayloadAction<AppError>) => {
      setApiErrorState(state.updateProfileApi, action.payload);
    },
    updateProfileReset: (state) => {
      resetApiState(state.updateProfileApi);
    },
    removeProfile: (state) => {
      state.profile = undefined;
    },
  },
});

export default accountSlice;
