import React from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';

import { useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { sharedSlice } from '../../index';

const Home = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  useMount(() => {
    dispatch(sharedSlice.actions.setDisplayPath({ title: t(Translations.Menu.Home), fontawesomeIcon: 'home', disableAutoLastBreadcrumb: true }));

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  });

  return <>{t(Translations.Menu.Home)}</>;
};

export default Home;
