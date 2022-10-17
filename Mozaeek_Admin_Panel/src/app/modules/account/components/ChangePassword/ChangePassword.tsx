import { Alert, Button, Form, Space } from 'antd';
import React from 'react';
import { useTranslation } from 'react-i18next';

import { useAntdValidation, useHttpCall } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { AppErrorAlert, PasswordInput } from '../../../shared';
import { changePasswordHttp } from '../../http/account-http';

type FormValues = Readonly<{
  currentPassword: string;
  newPassword: string;
  confirmNewPassword: string;
}>;

const ChangePassword: React.VFC = () => {
  const { t } = useTranslation();

  const changePasswordApi = useHttpCall(changePasswordHttp);

  const [formInstance] = Form.useForm<FormValues>();
  const { labelWithRules } = useAntdValidation(formInstance);

  const initialValues: FormValues = {
    currentPassword: '',
    newPassword: '',
    confirmNewPassword: '',
  };

  const handleSubmit = async (values: FormValues) => {
    await changePasswordApi.call({ currentPassword: values.currentPassword, newPassword: values.newPassword });
  };

  return (
    <>
      <AppErrorAlert error={changePasswordApi.error} />
      <Form<FormValues> form={formInstance} initialValues={initialValues} layout={'horizontal'} labelCol={{ span: 4 }} onFinish={handleSubmit}>
        <Form.Item name="currentPassword" {...labelWithRules({ label: t(Translations.Account.CurrentPassword), rules: [{ type: 'Required' }] })}>
          <PasswordInput />
        </Form.Item>
        <Form.Item name="newPassword" {...labelWithRules({ label: t(Translations.Account.NewPassword), rules: [{ type: 'Required' }] })}>
          <PasswordInput />
        </Form.Item>
        <Form.Item
          name="confirmNewPassword"
          {...labelWithRules({
            label: t(Translations.Account.ConfirmNewPassword),
            rules: [
              { type: 'Required' },
              { type: 'MatchField', arguments: [{ matchFieldName: 'newPassword', rejectionMessage: t(Translations.Account.PasswordsShouldMatch) }] },
            ],
          })}
          dependencies={['newPassword']}>
          <PasswordInput />
        </Form.Item>
        <Form.Item>
          <Space>
            <Button size={'large'} type="primary" htmlType="submit" loading={changePasswordApi.pending}>
              {t(Translations.Common.Submit)}
            </Button>
            {changePasswordApi.success && <Alert type="success" message={t(Translations.Account.PasswordChangedSuccessfully)} />}
          </Space>
        </Form.Item>
      </Form>
    </>
  );
};

export default ChangePassword;
