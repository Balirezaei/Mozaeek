import { EyeInvisibleOutlined, EyeTwoTone, KeyOutlined, UserOutlined } from '@ant-design/icons';
import { Button, Form, Input } from 'antd';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';

import { LocalStorageKey } from '../../../../features/constants';
import { useAntdValidation, useHttpCall } from '../../../../features/hooks';
import { Translations } from '../../../../features/localization';
import { isDevelopment, setStorage, toJson } from '../../../../utils/helpers';
import { userLoginHttp } from '../../../http/users/users-http';
import { AccountLoginRs } from '../../../http/users/usersApiTypes';
import { AppErrorAlert, successfulResponse } from '../../shared';
import { decodeAppJwt } from '../../shared/helpers/AppJwtHelpers';
import { authenticationSlice } from '../index';

type FormValues = Readonly<{
  username: string;
  password: string;
}>;

const Login: React.VFC = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const history = useHistory();

  const loginApi = useHttpCall(userLoginHttp);

  const [formInstance] = Form.useForm();
  const { labelWithRules } = useAntdValidation(formInstance);

  const initialValues: FormValues = {
    username: '',
    password: '',
  };

  const handleSubmit = (v: any) => {
    const values = v as FormValues;
    loginApi.call<AccountLoginRs>({ username: values.username, password: values.password }).then((response) => {
      if (successfulResponse(response)) {
        setStorage(LocalStorageKey.AuthToken, toJson(response!.data)!);
        dispatch(authenticationSlice.actions.setAuthToken(response!.data));
        const jwt = decodeAppJwt(response!.data.token)!;
        dispatch(authenticationSlice.actions.login({ ...response!.data, date: Date.now(), role: jwt.UserRole, username: values.username }));
        history.push('/');
      }
    });
  };

  return (
    <>
      <div style={{ maxWidth: 400, width: '100%' }}>
        <AppErrorAlert error={loginApi.error} />
        <Form form={formInstance} initialValues={initialValues} onFinish={handleSubmit} layout="vertical">
          <Form.Item className="white-label" name="username" {...labelWithRules({ label: t(Translations.Auth.Username), rules: [{ type: 'Required' }] })}>
            <Input
              prefix={<UserOutlined />}
              type="text"
              placeholder={t(Translations.Auth.EnterYourUsername)}
              size="large"
              dir="ltr"
              className="english"
              disabled={loginApi.pending}
            />
          </Form.Item>
          <Form.Item className="white-label" name="password" {...labelWithRules({ label: t(Translations.Auth.Password), rules: [{ type: 'Required' }] })}>
            <Input.Password
              disabled={loginApi.pending}
              prefix={<KeyOutlined />}
              iconRender={(visible) => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
              placeholder={t(Translations.Auth.EnterYourPassword)}
              size="large"
              dir="ltr"
              className="english"
            />
          </Form.Item>
          <div className="d-flex flex-wrap justify-content-between align-items-center">
            {/*<Link className={'text-white'} to="/auth/forgot-password">*/}
            {/*  {t(Translations.Auth.ForgotPassword)}*/}
            {/*</Link>*/}
            <Button className="antd-gold6-btn" htmlType="submit" type="primary" loading={loginApi.pending} size="large">
              {t(Translations.Auth.SignIn)}
            </Button>
            {isDevelopment() && (
              <Button
                htmlType="button"
                type="dashed"
                size="large"
                onClick={() => {
                  localStorage.clear();
                  window.location.reload();
                }}>
                Reset App
              </Button>
            )}
          </div>
        </Form>
      </div>
    </>
  );
};

export default Login;
