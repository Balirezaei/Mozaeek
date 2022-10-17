import propTypes from 'prop-types';
import React, { useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';

import { LocalStorageKey } from '../../../../../features/constants';
import { useAppSelector } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { removeStorage } from '../../../../../utils/helpers';
import { accountSlice, refreshAuthToken } from '../../../account';
import authenticationSlice from '../../../authentication/redux/authentication-slice';
import { AppErrorAlert, initializeAppSaga } from '../../index';

type Props = {
  loading: React.ReactElement;
};
const Initialization: React.FC<Props> = (props) => {
  const { t } = useTranslation();
  const history = useHistory();
  const dispatch = useDispatch();

  const initialized = useAppSelector((state) => state.shared.initialized);
  const initializationError = useAppSelector((state) => state.shared.initializationError);

  // //Warn about initialization time cost
  // useEffect(() => {
  //   const timer = setTimeout(() => {
  //     if (!initializedRef.current) {
  //       console.error('Warning: Initialization took too long.');
  //     }
  //   }, 3000);
  //   return () => clearTimeout(timer);
  //   // don't need to run even after initialized variable changed
  //   // eslint-disable-next-line react-hooks/exhaustive-deps
  // }, []);

  useEffect(() => {
    // dispatch(getIPLocationSaga());
    dispatch(initializeAppSaga({ history }));
  }, [dispatch, history]);

  useEffect(() => {
    if (initializationError) {
      removeStorage(LocalStorageKey.AuthToken);
      dispatch(accountSlice.actions.removeProfile());
      dispatch(authenticationSlice.actions.logout());
    }
  }, [dispatch, initializationError]);

  useEffect(() => {
    if (initialized) {
      refreshAuthToken().then();
    }
  }, [initialized]);

  if (initializationError) {
    return (
      <div className="text-center mx-20 my-10">
        <h3 className="text-danger">{t(Translations.Common.Error)}</h3>
        <h6 dir="ltr" className="english">
          {initializationError?.initializationState}
        </h6>
        <div className="mx-20">
          <AppErrorAlert showCode error={initializationError?.abpError} />
        </div>
      </div>
    );
  }

  return !initialized ? props.loading : <>{props.children}</>;
};

Initialization.propTypes = {
  loading: propTypes.element.isRequired,
};

export default Initialization;
