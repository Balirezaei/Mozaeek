import { EditOutlined } from '@ant-design/icons';
import { Button, Space, Table, Tag, Tooltip } from 'antd';
import { ColumnsType } from 'antd/es/table';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useHttpCall, useManualRerender } from '../../../../../../features/hooks';
import { Translations } from '../../../../../../features/localization';
import { GetAllRequestBase } from '../../../../../../types';
import { AnnouncementRequestItem, AnnouncementRequestsGetAllRs } from '../../../../../http/core/core-apiTypes';
import { announcementGetAllAnnouncementRequestHttp } from '../../../../../http/core/core-http';
import { ApiModule } from '../../../../apiModule';
import { AppErrorAlert, successfulResponse, useAntdTable } from '../../../../shared';

type Props = {
  refreshTime: number;
};
const AnnouncementRequestsList: React.VFC<Props> = React.memo((props: Props) => {
  const { t } = useTranslation();

  const manualRerender = useManualRerender();
  const table = useAntdTable<AnnouncementRequestItem>(ApiModule.AnnouncementRequests);

  const getAllApi = useHttpCall(announcementGetAllAnnouncementRequestHttp);

  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);
  // const [requestConnectionsModalData, updateRequestConnectionsModalData] = useImmer<{ title?: string; requestId?: number; visible: boolean }>({
  //   visible: false,
  // });

  useEffect(() => {
    getAll();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.refreshTime, request, manualRerender.rerenderData]);

  const getAll = () => {
    getAllApi.call<AnnouncementRequestsGetAllRs>({ ...request }).then((response) => {
      if (successfulResponse(response)) {
        table.setData(response!.data);
      }
    });
  };

  const columns: ColumnsType<AnnouncementRequestItem> = [
    {
      title: t(Translations.Common.Title),
      dataIndex: 'title',
      ellipsis: { showTitle: false },
      render: (value) => (
        <Tooltip placement="topRight" title={value}>
          {value}
        </Tooltip>
      ),
    },
    {
      title: t(Translations.Core.RequestTarget),
      dataIndex: 'requestTargetTitle',
    },
    {
      title: t(Translations.Core.Labels),
      dataIndex: 'requestTargetLabels',
      render: (value: string[]) => value?.map((item) => <Tag key={item}>{item}</Tag>),
    },
    {
      title: t(Translations.Core.RequestOrganizations),
      dataIndex: 'requestTargetRequestOrgs',
      render: (value: string[]) => value?.map((item) => <Tag key={item}>{item}</Tag>),
    },
    {
      title: t(Translations.Core.Subjects),
      dataIndex: 'requestTargetSubjects',
      render: (value: string[]) => value?.map((item) => <Tag key={item}>{item}</Tag>),
    },
    {
      title: t(Translations.Core.Points),
      dataIndex: 'points',
      render: (value: string[]) => value?.map((item) => <Tag key={item}>{item}</Tag>),
    },
    {
      title: t(Translations.Common.PublishDate),
      dataIndex: 'publishDate',
    },
    {
      width: '5%',
      render: () => (
        <Space>
          <Button htmlType="button" size="small" type="primary" icon={<EditOutlined />} className="antd-gold6-btn" />
        </Space>
      ),
    },
  ];
  table.configColumns(columns);
  table.setRequestDataChangeFn(setRequest);

  return (
    <>
      {getAllApi.error ? (
        <AppErrorAlert error={getAllApi.error} disableAutoHide />
      ) : (
        <Table {...table.tableProps} columns={columns} loading={getAllApi.pending} />
      )}
    </>
  );
});

export default AnnouncementRequestsList;
