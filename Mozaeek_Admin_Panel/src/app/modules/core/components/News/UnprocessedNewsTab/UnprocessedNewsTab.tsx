import { EditOutlined, EyeInvisibleOutlined, LinkOutlined } from '@ant-design/icons';
import { Button, Popconfirm, Space, Table, TableProps } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { useHttpCall, useManualRerender } from '../../../../../../features/hooks';
import { Translations } from '../../../../../../features/localization';
import { GetAllRequestBase } from '../../../../../../types';
import { toQueryString } from '../../../../../../utils/helpers';
import { announcementGetAllNewsReadyToProcessHttp, announcementRssNewsChangeStateHttp } from '../../../../../http/core/core-http';
import { ApiModule } from '../../../../apiModule';
import { AppErrorAlert, successfulResponse, useAntdTable } from '../../../../shared';
import { AnnouncementGetAllNewsReadyToProcessResponse, AnnouncementNewsItem } from '../../../apiTypes';
import { CorePaths } from '../../../CoreRoutes';
import { AnnouncementNewsCreaditQueryString } from '../../../types';

type Props = {};
const UnprocessedNewsTab: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();

  const manualRerender = useManualRerender();
  const table = useAntdTable<AnnouncementNewsItem>(ApiModule.UnprocessedNews);

  const getAllNewsReadyToProcessApi = useHttpCall(announcementGetAllNewsReadyToProcessHttp);
  const changeStateApi = useHttpCall(announcementRssNewsChangeStateHttp);

  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);

  useEffect(() => {
    getAllNews();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [manualRerender.rerenderData, request]);

  useEffect(() => {
    setRequest(table.requestData);
  }, [table.requestData]);

  const getAllNews = () => {
    getAllNewsReadyToProcessApi.call<AnnouncementGetAllNewsReadyToProcessResponse>({ ...request }).then((response) => {
      if (successfulResponse(response)) {
        table.setData(response!.data);
      }
    });
  };

  const handleChangeStateClicked = (id: number) => {
    changeStateApi.call<undefined>({ id: id }).then((response) => {
      if (successfulResponse(response)) {
        manualRerender.rerender();
      }
    });
  };

  const columns: TableProps<AnnouncementNewsItem>['columns'] = [
    {
      title: t(Translations.Common.Title),
      dataIndex: 'title',
    },
    {
      title: t(Translations.Common.Source),
      dataIndex: 'source',
    },
    {
      title: t(Translations.Common.Date),
      dataIndex: 'createDate',
    },
    {
      render: (value, record) => (
        <Space>
          <Link target="_blank" to={`${CorePaths.NewsCreadit}?${toQueryString({ newsId: record.id.toString() } as AnnouncementNewsCreaditQueryString)}`}>
            <Button type="primary" htmlType="button" icon={<EditOutlined />} size="small" />
          </Link>
          <a rel="noopener noreferrer" href={record.link} target="_blank">
            <Button type="default" htmlType="button" icon={<LinkOutlined />} size="small" />
          </a>
          <Popconfirm
            title={t(Translations.Common.AreYouSureDeleteThisItemVar, { item: t(Translations.Core.NewsSingular) })}
            okText={t(Translations.Common.Delete)}
            okButtonProps={{ loading: changeStateApi.pending, danger: true }}
            onConfirm={() => handleChangeStateClicked(record.id)}>
            <Button htmlType="button" size="small" type="primary" danger icon={<EyeInvisibleOutlined />} />
          </Popconfirm>
        </Space>
      ),
    },
  ];
  table.configColumns(columns);

  return (
    <>
      <AppErrorAlert error={getAllNewsReadyToProcessApi.error || changeStateApi.error} disableAutoHide />
      <Table {...table.tableProps} size="small" columns={columns} loading={getAllNewsReadyToProcessApi.pending} />
    </>
  );
});

export default UnprocessedNewsTab;
