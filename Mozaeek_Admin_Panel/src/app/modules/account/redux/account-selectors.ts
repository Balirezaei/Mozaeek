import { RootState } from '../../../../features/redux';

export const profileSelector = (state: RootState) => {
  return state.account.profile!;
};

export const cultureSelector = (state: RootState) => {
  return state.account.culture;
};
