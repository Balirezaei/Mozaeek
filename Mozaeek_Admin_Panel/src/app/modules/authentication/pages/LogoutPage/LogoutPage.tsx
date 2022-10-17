import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { Redirect } from 'react-router-dom';

import { LayoutSplashScreen } from '../../../../../_metronic/layout';
import { LocalStorageKey } from '../../../../../features/constants';
import { useAppSelector } from '../../../../../features/hooks';
import { removeStorage } from '../../../../../utils/helpers';
import { accountSlice } from '../../../account';
import authenticationSlice from '../../redux/authentication-slice';

const LogoutPage = () => {
  const dispatch = useDispatch();
  const isAuthenticated = useAppSelector((state) => state.authentication.isAuthenticated);

  useEffect(() => {
    removeStorage(LocalStorageKey.AuthToken);
    localStorage.removeItem('TempLogin');
    //dispatch(accountSlice.actions.removeProfile());
    dispatch(authenticationSlice.actions.logout());
  }, [dispatch]);
  return <>{isAuthenticated ? <LayoutSplashScreen /> : <Redirect to="/auth/login" />};</>;
};

export default LogoutPage;
