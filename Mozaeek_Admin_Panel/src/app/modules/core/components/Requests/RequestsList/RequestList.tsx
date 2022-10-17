import { EditOutlined } from '@ant-design/icons';
import { Button, Space, Table } from 'antd';
import { ColumnsType } from 'antd/es/table';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { useHttpCall, useManualRerender } from '../../../../../../features/hooks';
import { Translations } from '../../../../../../features/localization';
import { GetAllRequestBase } from '../../../../../../types';
import { toQueryString } from '../../../../../../utils/helpers';
import { requestDeleteHttp, requestGetAllHttp } from '../../../../../http/core/core-http';
import { ApiModule } from '../../../../apiModule';
import { AppErrorAlert, PopconfirmDelete, successfulResponse, useAntdTable } from '../../../../shared';
import { RequestGetAllResponse, RequestItem } from '../../../apiTypes';
import { CorePaths } from '../../../CoreRoutes';

type Props = {
  refreshTime: number;
};
const RequestList: React.VFC<Props> = React.memo((props: Props) => {
  const { t } = useTranslation();

  const manualRerender = useManualRerender();
  const table = useAntdTable<RequestItem>(ApiModule.Requests);

  const getAllApi = useHttpCall(requestGetAllHttp);
  const deleteApi = useHttpCall(requestDeleteHttp);

  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);
  // const [requestConnectionsModalData, updateRequestConnectionsModalData] = useImmer<{ title?: string; requestId?: number; visible: boolean }>({
  //   visible: false,
  // });

  const handleDeleteClicked = (id: number) => {
    deleteApi.call({ id: id }).then((response) => {
      if (successfulResponse(response)) {
        manualRerender.rerender();
      }
    });
  };

  useEffect(() => {
    getAll();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.refreshTime, request, manualRerender.rerenderData]);

  const getAll = () => {
    deleteApi.reset();

    getAllApi.call<RequestGetAllResponse>({ ...request }).then((response) => {
      if (successfulResponse(response)) {
        table.setData(response!.data);
      }
    });
  };

  const columns: ColumnsType<RequestItem> = [
    {
      title: t(Translations.Common.Title),
      dataIndex: 'title',
    },
    {
      render: (value, record) => (
        <Space>
          <PopconfirmDelete itemName={t(Translations.Core.Request)} pending={deleteApi.pending} data={record.id} onDelete={handleDeleteClicked} />
          <Link to={`${CorePaths.RequestCreadit}?${toQueryString({ id: record.id })}`}>
            <Button htmlType="button" size="small" type="primary" icon={<EditOutlined />} className="antd-gold6-btn" />
          </Link>
          {/*<Button htmlType="button" size="small" type="default" icon={<LinkOutlined />} onClick={() => handleRequestConnectClicked(record.id, record.title)} />*/}
        </Space>
      ),
    },
  ];
  table.configColumns(columns);
  table.setRequestDataChangeFn(setRequest);

  return (
    <>
      <AppErrorAlert error={deleteApi.error} />

      {getAllApi.error ? (
        <AppErrorAlert error={getAllApi.error} disableAutoHide />
      ) : (
        <Table {...table.tableProps} columns={columns} loading={getAllApi.pending} />
      )}
    </>
  );
});

export default RequestList;
