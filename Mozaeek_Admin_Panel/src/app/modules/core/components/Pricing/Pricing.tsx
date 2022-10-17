import { PlusOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Card, Space, Table } from 'antd';
import { ColumnsType } from 'antd/es/table';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { Link } from 'react-router-dom';

import { useGlobalization, useHttpCall, useManualRerender, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { ApiResponse, GetAllRequestBase } from '../../../../../types';
import { toQueryString } from '../../../../../utils/helpers';
import { PriceItem } from '../../../../http/core/core-apiTypes';
import { AppListBaseResponse } from '../../../../mosaik';
import { ApiModule } from '../../../apiModule';
import { AppErrorAlert, EditTableActionButton, PopconfirmDelete, Status, sharedSlice, successfulResponse, useAntdTable } from '../../../shared';

// eslint-disable-next-line @typescript-eslint/no-unused-vars
type Props<TItem, TGetAllRs> = {
  apiModule: ApiModule;
  http: {
    getAll: (data: GetAllRequestBase) => Promise<ApiResponse<TGetAllRs>>;
    delete: (data: { id: number }) => Promise<ApiResponse<any>>;
  };
  translationKeys: {
    moduleType: string;
  };
  icon: string;
  creaditPath: string;
};
function Pricing<TItem extends PriceItem, TGetAllRs extends AppListBaseResponse<TItem>>(props: Props<TItem, TGetAllRs>) {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  const manualRerender = useManualRerender();
  const { formatShortDate } = useGlobalization();
  const table = useAntdTable<TItem>(props.apiModule);

  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);

  const getAllApi = useHttpCall(props.http.getAll);
  const deleteApi = useHttpCall(props.http.delete);

  const getAll = () => {
    getAllApi.call<TGetAllRs>({ ...request }).then((response) => {
      if (successfulResponse(response)) {
        table.setData(response!.data);
      }
    });
  };

  useMount(() => {
    dispatch(
      sharedSlice.actions.setDisplayPath({
        title: t(props.translationKeys.moduleType),
        breadcrumbs: [
          {
            title: t(Translations.Core.Pricing),
          },
        ],
        fontawesomeIcon: props.icon,
      })
    );

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  });

  useEffect(() => {
    getAll();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [request, manualRerender.rerenderData]);

  const handleDeleteClicked = (id: number) => {
    deleteApi.call({ id: id }).then((response) => {
      if (successfulResponse(response)) {
        manualRerender.rerender();
      }
    });
  };

  const columns: ColumnsType<TItem> = [
    { title: t(Translations.Common.Title), dataIndex: 'title' },
    { title: t(Translations.Common.StartDate), dataIndex: 'startDate', render: (value) => formatShortDate(value) },
    { title: t(Translations.Common.Status), dataIndex: 'isActive', render: (value) => <Status type="icon" status={value} /> },
    {
      render: (value, record) => (
        <Space>
          <PopconfirmDelete itemName={t(Translations.Core.Pricing)} pending={deleteApi.pending} data={record.id} onDelete={handleDeleteClicked} />
          <Link to={`${props.creaditPath}?${toQueryString({ id: record.id })}`}>
            <EditTableActionButton />
          </Link>
        </Space>
      ),
    },
  ];
  table.configColumns(columns);
  table.setRequestDataChangeFn(setRequest);

  return (
    <Card
      extra={
        <Space>
          <Link to={props.creaditPath}>
            <Button type="primary" htmlType="button" icon={<PlusOutlined />}>
              {t(Translations.Common.Create)}
            </Button>
          </Link>
          <Button type="default" htmlType="button" icon={<ReloadOutlined />} onClick={() => manualRerender.rerender()}>
            {t(Translations.Common.Refresh)}
          </Button>
        </Space>
      }>
      {getAllApi.error ? (
        <AppErrorAlert error={getAllApi.error} disableAutoHide />
      ) : (
        <>
          <AppErrorAlert error={deleteApi.error} />
          <Table {...table.tableProps} columns={columns} loading={getAllApi.pending} />
        </>
      )}
    </Card>
  );
}

export default React.memo(Pricing);
