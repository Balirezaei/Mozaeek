import React from 'react';
import { Link, Redirect, Route, Switch, useRouteMatch } from 'react-router-dom';

import { useAppSelector } from '../../../../../features/hooks';
import Login from '../../components/Login';

const AuthPage: React.VFC = () => {
  const isAuthenticated = useAppSelector((state) => state.authentication.isAuthenticated);
  const { path } = useRouteMatch();

  return (
    <>
      {isAuthenticated ? (
        <Redirect to="/" />
      ) : (
        <>
          <div className="d-flex flex-column flex-root position-relative overflow-hidden">
            {/*begin::Login*/}
            <div className="login login-3 login-signin-on d-flex flex-column flex-lg-row flex-row-fluid" id="kt_login">
              <svg version="1.1" className="mesh-svg" x="0px" y="0px" viewBox="0 0 1800 900">
                <line x1="67.5" y1="48.5" x2="171.5" y2="48.5" />
                <line x1="290.5" y1="48.5" x2="339.5" y2="48.5" />
                <line x1="448.5" y1="48.5" x2="498.5" y2="48.5" />
                <line x1="753.5" y1="48.5" x2="551.5" y2="48.5" />
                <path d="M918.5,48.5c13.3,0,26.5,0,39.8,0" />
                <path d="M1190.5,48.5c33.5,0,67,0,100.5,0" />
                <line x1="1299.5" y1="48.5" x2="1385.5" y2="48.5" />
                <line x1="1401.5" y1="48.5" x2="1403.5" y2="48.5" />
                <line x1="1414.5" y1="48.5" x2="1428.5" y2="48.5" />
                <line x1="1431.5" y1="48.5" x2="1685.5" y2="48.5" />
                <line x1="1690.5" y1="48.5" x2="1709.5" y2="48.5" />
                <line x1="1657.5" y1="149.5" x2="1654.5" y2="149.5" />
                <line x1="1650.5" y1="149.5" x2="1631.5" y2="149.5" />
                <line x1="1166.5" y1="149.5" x2="1599.5" y2="149.5" />
                <path d="M1156.5,149.5c-18,0-36,0-54,0" />
                <line x1="919.5" y1="149.5" x2="960.5" y2="149.5" />
                <line x1="598.5" y1="149.5" x2="727.5" y2="149.5" />
                <line x1="521.5" y1="149.5" x2="495.5" y2="149.5" />
                <line x1="453.5" y1="149.5" x2="8.5" y2="149.5" />
                <line x1="153.5" y1="250.5" x2="377.5" y2="250.5" />
                <line x1="440.5" y1="250.5" x2="515.5" y2="250.5" />
                <line x1="918.5" y1="250.5" x2="853.5" y2="250.5" />
                <line x1="1563.5" y1="250.5" x2="948.5" y2="250.5" />
                <line x1="1568.5" y1="250.5" x2="1577.5" y2="250.5" />
                <line x1="1659.5" y1="250.5" x2="1651.5" y2="250.5" />
                <path id="h1" className="animation" d="M1797.7,352.8c-594.7-0.2-1189.4-0.3-1784.1-0.5" />
                <line x1="188.5" y1="453.5" x2="195.5" y2="453.5" />
                <line x1="204.5" y1="453.5" x2="301.5" y2="453.5" />
                <line x1="319.5" y1="453.5" x2="328.5" y2="453.5" />
                <line x1="364.5" y1="453.5" x2="379.5" y2="453.5" />
                <path d="M482.3,452.5c10.1,0,20.1,0,30.2,0" />
                <line x1="514.5" y1="452.5" x2="555.5" y2="452.5" />
                <line x1="558.5" y1="452.5" x2="570.5" y2="452.5" />
                <line x1="620.5" y1="452.5" x2="636.5" y2="452.5" />
                <line x1="775.5" y1="452.5" x2="1099.5" y2="452.5" />
                <line x1="1513.5" y1="452.5" x2="1109.5" y2="452.5" />
                <line x1="1716.5" y1="452.5" x2="1695.5" y2="452.5" />
                <path d="M1684.5,452.5c-8,0-16,0-24,0" />
                <path d="M1789.3,452.5c-12.9,0-25.9,0-38.8,0" />
                <path id="h2" className="animation" d="M0.7,554.4c599.8,0,1199.5,0,1799.3,0.1" />
                <path d="M42.3,656.5c23.7,0,47.4,0,71.2,0" />
                <line x1="367.5" y1="655.5" x2="628.5" y2="655.5" />
                <line x1="897.5" y1="655.5" x2="1051.5" y2="655.5" />
                <line x1="1429.5" y1="655.5" x2="1435.5" y2="655.5" />
                <line x1="1623.5" y1="655.5" x2="1672.5" y2="655.5" />
                <line x1="1683.5" y1="655.5" x2="1705.5" y2="655.5" />
                <line x1="1686.5" y1="756.5" x2="1474.5" y2="756.5" />
                <line x1="1322.5" y1="756.5" x2="1274.5" y2="756.5" />
                <line x1="1096.5" y1="756.5" x2="1073.5" y2="756.5" />
                <line x1="1027.5" y1="756.5" x2="909.5" y2="756.5" />
                <path d="M795.9,756.5c-2.5,0-4.9,0-7.4,0" />
                <line x1="573.5" y1="756.5" x2="435.5" y2="756.5" />
                <line x1="432.5" y1="858.5" x2="492.5" y2="858.5" />
                <line x1="1664.5" y1="858.5" x2="1603.5" y2="858.5" />
                <line x1="50.5" y1="120.5" x2="50.5" y2="184.5" />
                <path d="M50.5,569.6c0,21.6,0,43.2,0,64.9" />
                <line x1="50.5" y1="647.5" x2="50.5" y2="658.5" />
                <line x1="155.5" y1="37.5" x2="155.5" y2="74.5" />
                <line x1="155.5" y1="23.5" x2="155.5" y2="20.5" />
                <line x1="155.5" y1="149.5" x2="155.5" y2="242.5" />
                <line x1="155.5" y1="257.5" x2="155.5" y2="246.5" />
                <line x1="155.5" y1="280.5" x2="155.5" y2="287.5" />
                <line x1="155.5" y1="370.5" x2="155.5" y2="389.5" />
                <line x1="155.5" y1="391.5" x2="155.5" y2="396.5" />
                <line x1="155.5" y1="522.5" x2="155.5" y2="526.5" />
                <line x1="155.5" y1="544.5" x2="155.5" y2="541.5" />
                <line x1="155.5" y1="569.5" x2="155.5" y2="576.5" />
                <path d="M155.5,609.5c0,13.2,0,26.4,0,39.5" />
                <path id="v1" className="animation" d="M258.8,900c0.2-299.8,0.5-599.7,0.7-899.5" />
                <line x1="365.5" y1="5.5" x2="365.5" y2="37.5" />
                <line x1="363.5" y1="63.5" x2="363.5" y2="115.5" />
                <line x1="363.5" y1="134.5" x2="363.5" y2="214.5" />
                <line x1="363.5" y1="248.5" x2="363.5" y2="450.5" />
                <line x1="363.5" y1="491.5" x2="363.5" y2="496.5" />
                <line x1="363.5" y1="572.5" x2="363.5" y2="578.5" />
                <line x1="467.5" y1="895.5" x2="467.5" y2="562.5" />
                <line x1="467.5" y1="355.5" x2="467.5" y2="326.5" />
                <line x1="467.5" y1="208.5" x2="467.5" y2="323.5" />
                <line x1="467.5" y1="177.5" x2="467.5" y2="186.5" />
                <line x1="467.5" y1="124.5" x2="467.5" y2="79.5" />
                <line x1="467.5" y1="63.5" x2="467.5" y2="44.5" />
                <line x1="467.5" y1="30.5" x2="467.5" y2="37.5" />
                <line x1="571.5" y1="9.5" x2="571.5" y2="58.5" />
                <line x1="571.5" y1="357.5" x2="571.5" y2="360.5" />
                <line x1="571.5" y1="454.5" x2="571.5" y2="474.5" />
                <path d="M571.4,478.1c0,10.5,0.1,20.9,0.1,31.4" />
                <line x1="571.5" y1="531.5" x2="571.5" y2="534.5" />
                <line x1="571.5" y1="631.5" x2="571.5" y2="759.5" />
                <line x1="676.5" y1="6.5" x2="676.5" y2="179.5" />
                <line x1="676.5" y1="530.5" x2="676.5" y2="533.5" />
                <line x1="676.5" y1="573.5" x2="676.5" y2="575.5" />
                <path d="M676.5,883.5c0-20.7,0-41.5,0-62.2" />
                <line x1="780.5" y1="780.5" x2="780.5" y2="776.5" />
                <line x1="780.5" y1="595.5" x2="780.5" y2="436.5" />
                <line x1="780.5" y1="403.5" x2="780.5" y2="392.5" />
                <line x1="780.5" y1="373.5" x2="780.5" y2="370.5" />
                <line x1="780.5" y1="313.5" x2="780.5" y2="291.5" />
                <path id="v2" className="animation" d="M885.5,1.5c0.4,298.1,0.8,596.2,1.2,894.2" />
                <line x1="989.5" y1="804.5" x2="989.5" y2="446.5" />
                <line x1="989.5" y1="385.5" x2="989.5" y2="413.5" />
                <line x1="989.5" y1="331.5" x2="989.5" y2="189.5" />
                <line x1="989.5" y1="157.5" x2="989.5" y2="182.5" />
                <line x1="989.5" y1="37.5" x2="989.5" y2="34.5" />
                <line x1="989.5" y1="14.5" x2="989.5" y2="19.5" />
                <line x1="1093.5" y1="334.5" x2="1093.5" y2="149.5" />
                <line x1="1093.5" y1="135.5" x2="1093.5" y2="142.5" />
                <line x1="1093.5" y1="379.5" x2="1093.5" y2="548.5" />
                <line x1="1093.5" y1="564.5" x2="1093.5" y2="609.5" />
                <line x1="1093.5" y1="708.5" x2="1093.5" y2="765.5" />
                <line x1="1302.5" y1="45.5" x2="1302.5" y2="526.5" />
                <line x1="1302.5" y1="750.5" x2="1302.5" y2="761.5" />
                <line x1="1302.5" y1="668.5" x2="1302.5" y2="674.5" />
                <path d="M1302.5,737.5c0-4.4,0-8.9-0.1-13.3c0-9.8-0.1-19.6-0.2-29.3" />
                <line x1="1406.5" y1="628.5" x2="1406.5" y2="609.5" />
                <line x1="1406.5" y1="589.5" x2="1406.5" y2="576.5" />
                <line x1="1406.5" y1="548.5" x2="1406.5" y2="37.5" />
                <line x1="1510.5" y1="25.5" x2="1510.5" y2="373.5" />
                <line x1="1510.5" y1="442.5" x2="1510.5" y2="461.5" />
                <line x1="1510.5" y1="438.5" x2="1510.5" y2="432.5" />
                <line x1="1510.5" y1="569.5" x2="1510.5" y2="573.5" />
                <path d="M1510.5,672.5c0-19,0-38,0-57" />
                <line x1="1510.5" y1="739.5" x2="1510.5" y2="830.5" />
                <line x1="1614.5" y1="872.5" x2="1614.5" y2="717.5" />
                <line x1="1517.5" y1="655.5" x2="1521.5" y2="655.5" />
                <line x1="1614.5" y1="697.5" x2="1614.5" y2="693.5" />
                <line x1="1614.5" y1="651.5" x2="1614.5" y2="637.5" />
                <line x1="1614.5" y1="551.5" x2="1614.5" y2="538.5" />
                <path d="M1614.5,472.5c0,13.7,0.1,27.4,0.1,41.1" />
                <line x1="1614.5" y1="336.5" x2="1614.5" y2="327.5" />
                <line x1="1614.5" y1="135.5" x2="1614.5" y2="6.5" />
                <line x1="1614.5" y1="515.5" x2="1614.5" y2="518.5" />
                <line x1="1718.5" y1="397.5" x2="1718.5" y2="400.5" />
                <path d="M1718.7,407.1c-0.1,11.1-0.1,22.3-0.2,33.4" />
                <line x1="1718.5" y1="453.5" x2="1718.5" y2="455.5" />
                <path d="M1718.5,562.5c0-28.3,0-56.7,0-85" />
                <line x1="1718.5" y1="584.5" x2="1718.5" y2="582.5" />
                <line x1="1718.5" y1="597.5" x2="1718.5" y2="594.5" />
                <path id="v3" className="animation" d="M1197.5,3.5c0.4,298.1,0.8,596.2,1.2,894.2" />
              </svg>

              {/*begin::Aside*/}
              <div
                className="login-aside d-flex flex-row-auto bgi-size-cover bgi-no-repeat p-10 p-lg-10"
                // style={{
                //   backgroundImage: `url(${toAbsoluteUrl('/media/bg/bg-4.jpg')})`,
                // }}
              >
                {/*begin: Aside Container*/}
                <div className="d-flex flex-row-fluid flex-column justify-content-between position-relative">
                  {/* start:: Aside header */}
                  <Link to="/" className="login-logo-holder">
                    {/*<img alt="Logo" className="login-logo" src={toAbsoluteUrl('/media/logos/logo.jpeg')} />*/}
                  </Link>
                  {/* end:: Aside header */}

                  {/* start:: Aside content */}
                  <div className="flex-column-fluid d-flex flex-column justify-content-center">
                    <h3 className="font-size-h1 mb-5 text-white">به سیستم موزاییک خوش آمدید!</h3>
                  </div>
                  {/* end:: Aside content */}
                </div>
                {/*end: Aside Container*/}
              </div>
              {/*begin::Aside*/}

              {/*begin::Content*/}
              <div className="flex-row-fluid d-flex flex-column position-relative p-7 overflow-hidden z-2">
                {/* begin::Content body */}
                <div className="d-flex flex-column-fluid flex-center mt-30 mt-lg-0 login-forms-holder">
                  <Switch>
                    <Route path={`${path}/login`}>
                      <Login />
                    </Route>
                    <Redirect exact from={`${path}`} to={`${path}/login`} />
                    <Redirect to={`${path}/login`} />
                  </Switch>
                </div>
                {/*end::Content body*/}
              </div>
              {/*end::Content*/}
            </div>
            {/* begin::Mobile footer */}
            <div className="login-footer">
              <div className="text-dark-50 font-weight-bold order-2 order-sm-1 my-2">&copy; 2021 Mosaik</div>
              {/*<div className="d-flex order-1 order-sm-2 my-2">*/}
              {/*  <Link to="/terms" className="text-dark-75 text-hover-primary">*/}
              {/*    Privacy*/}
              {/*  </Link>*/}
              {/*  <Link to="/terms" className="text-dark-75 text-hover-primary ml-4">*/}
              {/*    Legal*/}
              {/*  </Link>*/}
              {/*  <Link to="/terms" className="text-dark-75 text-hover-primary ml-4">*/}
              {/*    Contact*/}
              {/*  </Link>*/}
              {/*</div>*/}
            </div>
            {/* end::Mobile footer */}
            {/*end::Login*/}
          </div>
        </>
      )}
    </>
  );
};

export default AuthPage;
