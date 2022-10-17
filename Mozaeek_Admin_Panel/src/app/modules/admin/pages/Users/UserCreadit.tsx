import { RollbackOutlined, UserOutlined } from '@ant-design/icons';
import { Button, Card, Checkbox, Divider, Form, Input, Spin } from 'antd';
import generator from 'generate-password';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useLocation } from 'react-router';
import { Link, useHistory } from 'react-router-dom';

import { useAntdValidation, useHttpCall, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { CreaDitMode } from '../../../../../types';
import { fromQueryString } from '../../../../../utils/helpers';
import { userGetByIdHttp, userGetInitDtoHttp, userRegisterHttp, userUpdateHttp } from '../../../../http/users/users-http';
import { UserGetInitDtoRs, UserItem } from '../../../../http/users/usersApiTypes';
import { AppErrorAlert, FormItemActions, IconText, sharedSlice, successfulResponse } from '../../../shared';
import { AdminPath } from '../../AdminRoutes';

type UserCreaditFormValues = Readonly<{
  id: number;
  username: string;
  password: string;
  firstname: string;
  lastname: string;
  email: string;
  roles: string[];
  nationalCode: string;
}>;

const UserCreadit: React.VFC = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const location = useLocation();
  const history = useHistory();

  const queryStringObj = fromQueryString<{ id: number }>(location.search);
  const creaditMode: CreaDitMode = queryStringObj.id === undefined ? 'Create' : 'Edit';

  const [initDto, setInitDto] = useState<UserGetInitDtoRs>();

  const getByIdApi = useHttpCall(userGetByIdHttp);
  const getInitDtoApi = useHttpCall(userGetInitDtoHttp);
  const registerApi = useHttpCall(userRegisterHttp);
  const updateApi = useHttpCall(userUpdateHttp);

  const [form] = Form.useForm<UserCreaditFormValues>();
  const { labelWithRules } = useAntdValidation(form);

  useMount(() => {
    getInitDtoApi.call<UserGetInitDtoRs>(undefined).then((response) => {
      if (successfulResponse(response)) {
        setInitDto(response!.data);
      }
    });

    if (creaditMode === 'Create') {
      dispatch(
        sharedSlice.actions.setDisplayPath({
          title: t(Translations.Admin.CreateUser),
          breadcrumbs: [{ title: t(Translations.Admin.Admin) }, { title: t(Translations.Common.Users) }],
          fontawesomeIcon: 'user',
        })
      );

      form.setFieldsValue({
        password: generator.generate({ length: 12, lowercase: true, uppercase: true, numbers: true, strict: true }),
      });
    } else {
      dispatch(sharedSlice.actions.setDisplayPath('Skeleton'));

      getByIdApi.call<UserItem>({ id: +queryStringObj.id }).then((response) => {
        if (successfulResponse(response)) {
          const result = response!.data;
          form.setFieldsValue({
            username: result.userName,
            firstname: result.firstName,
            lastname: result.lastName,
            email: result.eMail,
            nationalCode: result.nationalCode,
            roles: result.roles,
          });

          dispatch(
            sharedSlice.actions.setDisplayPath({
              title: t(Translations.Admin.EditUser),
              breadcrumbs: [
                { title: t(Translations.Admin.Admin) },
                { title: t(Translations.Common.Users) },
                { title: t(Translations.Admin.EditUser) },
                { title: response!.data.userName },
              ],
              fontawesomeIcon: 'user',
              disableAutoLastBreadcrumb: true,
            })
          );
        } else {
          dispatch(sharedSlice.actions.setDisplayPath(null));
        }
      });
    }

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  });

  const handleSubmit = (values: UserCreaditFormValues) => {
    if (creaditMode === 'Create') {
      registerApi
        .call<undefined>({
          userName: values.username,
          password: values.password,
          firstName: values.firstname,
          lastName: values.lastname,
          nationalCode: values.nationalCode,
          eMail: values.email,
          role: values.roles.map((item) => +item)[0],
        })
        .then((response) => {
          if (successfulResponse(response)) {
            history.push(AdminPath.Users.Users);
          }
        });
    } else {
      updateApi
        .call<undefined>({
          userId: +queryStringObj.id,
          //password: values.password,
          firstName: values.firstname,
          lastName: values.lastname,
          nationalCode: values.nationalCode,
          eMail: values.email,
          role: values.roles.map((item) => +item)[0],
        })
        .then((response) => {
          if (successfulResponse(response)) {
            history.push(AdminPath.Users.Users);
          }
        });
    }
  };

  return (
    <>
      {getInitDtoApi.error || getByIdApi.error ? (
        <>
          <AppErrorAlert error={getInitDtoApi.error} />
          <AppErrorAlert error={getByIdApi.error} />
        </>
      ) : (
        <Card
          className="box-shadow"
          title={<IconText icon={<UserOutlined />} text={creaditMode === 'Create' ? t(Translations.Admin.CreateUser) : `${t(Translations.Admin.EditUser)}`} />}
          extra={
            <Link to="/admin/users">
              <Button icon={<RollbackOutlined />} type="default">
                {t(Translations.Common.BackToListVar, { page: t(Translations.Common.Users).toLowerCase() })}
              </Button>
            </Link>
          }>
          <AppErrorAlert error={registerApi.error || updateApi.error} />
          <Form form={form} onFinish={handleSubmit} labelCol={{ span: 4 }}>
            <Spin spinning={getByIdApi.pending || getInitDtoApi.pending}>
              <Form.Item name="username" {...labelWithRules({ label: t(Translations.Auth.Username), rules: [{ type: 'Required' }] })}>
                <Input disabled={creaditMode === 'Edit'} dir="ltr" allowClear />
              </Form.Item>
              <Form.Item name="password" {...labelWithRules({ label: t(Translations.Auth.Password), rules: [{ type: 'Required' }] })}>
                <Input dir="ltr" className="english" allowClear />
              </Form.Item>
              <Divider />
              <Form.Item name="firstname" {...labelWithRules({ label: t(Translations.Common.FirstName), rules: [{ type: 'Required' }] })}>
                <Input allowClear />
              </Form.Item>
              <Form.Item name="lastname" {...labelWithRules({ label: t(Translations.Common.LastName), rules: [{ type: 'Required' }] })}>
                <Input allowClear />
              </Form.Item>
              <Form.Item name="email" label={t(Translations.Common.Email)}>
                <Input allowClear />
              </Form.Item>
              <Form.Item name="nationalCode" label={t(Translations.Common.NationalID)}>
                <Input allowClear />
              </Form.Item>
              <Form.Item name="roles" {...labelWithRules({ label: t(Translations.Admin.Roles), rules: [{ type: 'Required' }] })}>
                <Checkbox.Group>
                  {initDto?.roles.map((item) => (
                    <Checkbox key={item.value} value={item.value}>
                      {item.text}
                    </Checkbox>
                  ))}
                </Checkbox.Group>
              </Form.Item>
            </Spin>
            <FormItemActions formInstance={form} creaditMode={creaditMode} submitPending={registerApi.pending || updateApi.pending} resetButton={false} />
          </Form>
        </Card>
      )}
    </>
  );
});

export default UserCreadit;
