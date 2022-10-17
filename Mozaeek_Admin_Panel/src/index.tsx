import { ConfigProvider } from 'antd';
import { enableMapSet } from 'immer';
import React, { Suspense } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';

import { LayoutSplashScreen, MetronicLayoutProvider, MetronicSplashScreenProvider } from './_metronic/layout';
import { CoreAxiosData, UsersAxiosData } from './app/http';
import { AccountAxiosData } from './app/modules/account';
import Routes from './app/modules/Routes';
import { App, AppCssImporter, startAppIntervals } from './app/modules/shared';
import Initialization from './app/modules/shared/components/Initialization/Initialization';
import MainErrorBoundary from './app/modules/shared/components/MainErrorBoundary/MainErrorBoundary';
import { TestAxiosData, setupAxiosInstances } from './features/http';
import { initLocalization } from './features/localization';
import { initializeStore, store } from './features/redux';
import { LocalizationHelpers, getAntdLocale } from './utils/helpers';

const criticalError = localStorage.getItem('__CE__');
localStorage.removeItem('__CE__');

enableMapSet();

initLocalization().then(() => {
  setupAxiosInstances([TestAxiosData, AccountAxiosData, CoreAxiosData, UsersAxiosData]);
});

initializeStore();

startAppIntervals({ refreshToken: true });

const htmlDirection = LocalizationHelpers.getHtmlDirection();

ReactDOM.render(
  criticalError ? (
    <>
      <h1 style={{ color: 'red' }}>Critical Error</h1>
      <h2>Contact Support</h2>
      <h3>{criticalError}</h3>
    </>
  ) : (
    // <React.StrictMode>
    <Provider store={store}>
      <MetronicLayoutProvider>
        <MetronicSplashScreenProvider>
          <AppCssImporter direction={htmlDirection}>
            <ConfigProvider direction={htmlDirection} locale={getAntdLocale().default}>
              <BrowserRouter>
                <MainErrorBoundary>
                  <Suspense fallback={<LayoutSplashScreen />}>
                    <Initialization loading={<LayoutSplashScreen />}>
                      <App>
                        <Routes />
                      </App>
                    </Initialization>
                  </Suspense>
                </MainErrorBoundary>
              </BrowserRouter>
            </ConfigProvider>
          </AppCssImporter>
        </MetronicSplashScreenProvider>
      </MetronicLayoutProvider>
    </Provider>
  ),
  // </React.StrictMode>,
  document.getElementById('root')
);
