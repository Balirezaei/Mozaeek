import { RootState } from '../../../../features/redux';

export const backendPreferencesSelector = (state: RootState) => {
  return state.shared.preferences.backend;
};
