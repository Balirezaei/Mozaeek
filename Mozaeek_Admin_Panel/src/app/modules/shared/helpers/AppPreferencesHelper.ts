import { LocalStorageKey } from '../../../../features/constants';
import { fromJson, getStorage, setStorage, toJson } from '../../../../utils/helpers';
import { Preferences } from '../types';

export const getPreferences = () => {
  let preferences: Preferences;
  const preferencesStorage = getStorage(LocalStorageKey.Preferences);
  if (preferencesStorage) {
    preferences = fromJson(preferencesStorage) as Preferences;
  } else {
    preferences = setDefaultPreferences();
  }
  return preferences;
};

export const setDefaultPreferences = () => {
  const preferences: Preferences = {
    backend: {
      api: {},
    },
    version: 1,
  };
  setStorage(LocalStorageKey.Preferences, toJson(preferences)!);
  return preferences;
};
