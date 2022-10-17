import get from 'lodash/get';
import set from 'lodash/set';
import { useDispatch } from 'react-redux';

import { LocalStorageKey } from '../../../../features/constants';
import { useAppSelector } from '../../../../features/hooks';
import { setStorage, toJson } from '../../../../utils/helpers';
import { ApiModule } from '../../apiModule';
import { appDefaults } from '../constants/appDefaults';
import { getPreferences, setDefaultPreferences } from '../helpers';
import { sharedSlice } from '../index';
import { backendPreferencesSelector } from '../redux/shared-selectors';

const usePreferences = () => {
  const backendPreferences = useAppSelector(backendPreferencesSelector);
  const dispatch = useDispatch();

  const getPreferenceApiPaginationOrDefault = (apiModule: ApiModule): { PageSize: number } => {
    let preferencePageSize = get(backendPreferences, ['api', apiModule, 'table', 'pageSize']);
    if (!preferencePageSize) {
      preferencePageSize = appDefaults.table.pagination.pageSize;
      setPreference(['backend', 'api', apiModule, 'table', 'pageSize'], preferencePageSize);
    }

    return {
      PageSize: preferencePageSize,
    };
  };

  const setApiPagination = (apiModule: ApiModule, value: { pageSize: number }) => {
    setPreference(['backend', 'api', apiModule, 'table', 'pageSize'], value.pageSize);
  };

  const setPreference = (path: string[], value: any) => {
    try {
      const preferences = getPreferences();
      set(preferences, path, value);
      setStorage(LocalStorageKey.Preferences, toJson(preferences)!);
      dispatch(sharedSlice.actions.setPreferences(preferences));
    } catch (e) {
      console.error('Setting preferences', e);
      setDefaultPreferences();
    }
  };

  return { getPreferenceApiPaginationOrDefault, setApiPagination, backendPreferences };
};

export default usePreferences;
