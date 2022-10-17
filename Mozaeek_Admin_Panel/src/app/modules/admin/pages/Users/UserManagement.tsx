import { PlusOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Card, Space, Table, Tag } from 'antd';
import { ColumnsType } from 'antd/es/table';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { Link } from 'react-router-dom';

import { useHttpCall, useManualRerender, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { GetAllRequestBase } from '../../../../../types';
import { userDeleteHttp, userGetAllHttp, userGetInitDtoHttp } from '../../../../http/users/users-http';
import { UserGetAllRs, UserGetInitDtoRs, UserItem } from '../../../../http/users/usersApiTypes';
import { ApiModule } from '../../../apiModule';
import { AppErrorAlert, IconText, PopconfirmDelete, sharedSlice, successfulResponse, useAntdTable } from '../../../shared';
import { AdminPath } from '../../AdminRoutes';

const UserManagement: React.VFC = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  const manualRerender = useManualRerender();
  const table = useAntdTable<UserItem>(ApiModule.Users);

  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);
  const [initDto, setInitDto] = useState<UserGetInitDtoRs>();

  const getAllApi = useHttpCall(userGetAllHttp);
  const deleteApi = useHttpCall(userDeleteHttp);
  const getInitDtoApi = useHttpCall(userGetInitDtoHttp);

  table.setRequestDataChangeFn(setRequest);

  useMount(() => {
    dispatch(
      sharedSlice.actions.setDisplayPath({
        title: t(Translations.Common.Users),
        breadcrumbs: [{ title: t(Translations.Admin.Admin) }],
        fontawesomeIcon: 'users',
      })
    );

    getInitDtoApi.call<UserGetInitDtoRs>(undefined).then((response) => {
      if (successfulResponse(response)) {
        setInitDto(response!.data);
      }
    });

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  });

  useEffect(() => {
    getAllUsers();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [request, manualRerender.rerenderData]);

  const getAllUsers = () => {
    getAllApi.call<UserGetAllRs>(request).then((response) => {
      if (successfulResponse(response)) {
        table.setData(response!.data);
      }
    });
  };

  const handleDeleteClicked = (id: number) => {
    deleteApi.call({ UserId: id }).then((response) => {
      if (successfulResponse(response)) {
        manualRerender.rerender();
      }
    });
  };

  const columns: ColumnsType<UserItem> = [
    {
      title: t(Translations.Auth.Username),
      dataIndex: 'userName',
    },
    {
      title: t(Translations.Common.FirstName),
      dataIndex: 'firstName',
    },
    {
      title: t(Translations.Common.LastName),
      dataIndex: 'lastName',
    },
    {
      title: t(Translations.Common.NationalID),
      dataIndex: 'nationalCode',
    },
    {
      title: t(Translations.Common.Email),
      dataIndex: 'eMail',
    },
    {
      title: t(Translations.Admin.Roles),
      dataIndex: 'roles',
      render: (value: number[]) => (
        <Space>
          {value?.map((item) => (
            <Tag key={item}>{initDto?.roles.find((f) => f.value === item.toString())?.text}</Tag>
          ))}
        </Space>
      ),
    },
    {
      render: (value, record) => (
        <Space>
          <PopconfirmDelete itemName={t(Translations.Common.User)} pending={deleteApi.pending} data={record.id} onDelete={handleDeleteClicked} />
          {/*<Link to={`${AdminPath.Users.UserCreadit}?${toQueryString({ id: record.id })}`}>*/}
          {/*  <Button htmlType="button" size="small" type="primary" icon={<EditOutlined />} className="antd-gold6-btn" />*/}
          {/*</Link>*/}
        </Space>
      ),
    },
  ];
  table.configColumns(columns);

  return (
    <>
      <Card
        className={'box-shadow'}
        title={<IconText icon={<i className="fas fa-users" />} text={t(Translations.Common.Users)} />}
        extra={
          <Space>
            <Link to={AdminPath.Users.UserCreadit}>
              <Button type="primary" htmlType="button" icon={<PlusOutlined />}>
                {t(Translations.Common.Create)}
              </Button>
            </Link>
            <Button type="default" htmlType="button" icon={<ReloadOutlined />} onClick={() => manualRerender.rerender()}>
              {t(Translations.Common.Refresh)}
            </Button>
          </Space>
        }>
        <AppErrorAlert error={deleteApi.error} />

        {getAllApi.error ? (
          <AppErrorAlert error={getAllApi.error} />
        ) : (
          <Table {...table.tableProps} columns={columns} loading={getAllApi.pending || getInitDtoApi.pending} />
        )}
      </Card>
    </>
  );
});

export default UserManagement;
