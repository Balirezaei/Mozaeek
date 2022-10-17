import { EditOutlined } from '@ant-design/icons';
import { Button, Space, Table, TableProps } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useHistory } from 'react-router-dom';

import { useHttpCall, useManualRerender } from '../../../../../../features/hooks';
import { Translations } from '../../../../../../features/localization';
import { GetAllRequestBase } from '../../../../../../types';
import { toQueryString } from '../../../../../../utils/helpers';
import { announcementDeleteHttp, announcementGetAllHttp } from '../../../../../http/core/core-http';
import { ApiModule } from '../../../../apiModule';
import { AppErrorAlert, PopconfirmDelete, successfulResponse, useAntdTable } from '../../../../shared';
import { AnnouncementGetAllResponse, AnnouncementItem } from '../../../apiTypes';
import { CorePaths } from '../../../CoreRoutes';
import { AnnouncementNewsCreaditQueryString } from '../../../types';

type Props = {};
const ProcessedNewsTab: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const history = useHistory();

  const manualRerender = useManualRerender();
  const table = useAntdTable<AnnouncementItem>(ApiModule.ProcessedNews);

  const getAllApi = useHttpCall(announcementGetAllHttp);
  const deleteApi = useHttpCall(announcementDeleteHttp);

  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);

  useEffect(() => {
    getAllNews();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [request, manualRerender.rerenderData]);

  useEffect(() => {
    setRequest(table.requestData);
  }, [table.requestData]);

  const getAllNews = () => {
    getAllApi.call<AnnouncementGetAllResponse>({ ...request }).then((response) => {
      if (successfulResponse(response)) {
        table.setData(response!.data);
      }
    });
  };

  const handleDeleteClicked = (id: number) => {
    deleteApi.call<undefined>({ id: id }).then((response) => {
      if (successfulResponse(response)) {
        manualRerender.rerender();
      }
    });
  };

  const handleEditClicked = (id: number) => {
    history.push(`${CorePaths.NewsCreadit}?${toQueryString({ id: id as unknown as string } as AnnouncementNewsCreaditQueryString)}`);
  };

  const columns: TableProps<AnnouncementItem>['columns'] = [
    {
      title: t(Translations.Common.Title),
      dataIndex: 'title',
    },
    {
      title: t(Translations.Common.PublishDate),
      dataIndex: 'publishDate',
    },
    {
      render: (value, record) => (
        <Space>
          <PopconfirmDelete itemName={t(Translations.Core.Announcement)} pending={deleteApi.pending} data={record.id} onDelete={handleDeleteClicked} />
          <Button htmlType="button" size="small" type="primary" icon={<EditOutlined />} className="antd-gold6-btn" onClick={() => handleEditClicked(record.id)} />
        </Space>
      ),
    },
  ];
  table.configColumns(columns);

  return (
    <>
      <AppErrorAlert error={getAllApi.error || deleteApi.error} />
      <Table {...table.tableProps} size="small" columns={columns} loading={getAllApi.pending} />
    </>
  );
});

export default ProcessedNewsTab;
