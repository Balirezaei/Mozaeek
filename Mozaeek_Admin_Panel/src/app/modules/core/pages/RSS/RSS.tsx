import { EditOutlined, InfoCircleOutlined, PlusOutlined } from '@ant-design/icons';
import { Button, Card, Space, Table, TableProps, Tooltip } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';

import { useHttpCall, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { GetAllRequestBase } from '../../../../../types';
import { rssDeleteHttp, rssGetAllParentsHttp } from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import { AppErrorAlert, PopconfirmDelete, Status, sharedSlice, successfulResponse, useAntdTable } from '../../../shared';
import { RssGetAllResponse, RssItem } from '../../apiTypes';
import { CorePaths } from '../../CoreRoutes';

type Props = {};
const RSS: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const history = useHistory();

  const table = useAntdTable<RssItem>(ApiModule.RSS);

  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);

  const rssGetAllApi = useHttpCall(rssGetAllParentsHttp);
  const rssDeleteApi = useHttpCall(rssDeleteHttp);

  useMount(() => {
    dispatch(
      sharedSlice.actions.setDisplayPath({
        breadcrumbs: [{ title: t(Translations.Core.Announcements) }],
        title: t(Translations.Core.RSS),
        fontawesomeIcon: 'rss',
      })
    );

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  });

  useEffect(() => {
    getRssItems();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [request]);

  useEffect(() => {
    setRequest(table.requestData);
  }, [table.requestData]);

  const getRssItems = () => {
    rssGetAllApi.call<RssGetAllResponse>(request).then((response) => {
      if (successfulResponse(response)) {
        table.setData(response!.data);
      }
    });
  };

  const handleAddClicked = () => {
    history.push(CorePaths.RssCreadit);
  };

  const handleEditClicked = (id: number) => {
    history.push(`${CorePaths.RssCreadit}?id=${id}`);
  };

  const handleDeleteClicked = (id: number) => {
    rssDeleteApi.call({ id: id }).then((response) => {
      if (response && response.error === null) {
        getRssItems();
      }
    });
  };

  const columns: TableProps<RssItem>['columns'] = [
    {
      title: (
        <Tooltip title={t(Translations.Core.HoverOverSourceTextToSeeItsUrl)}>
          {t(Translations.Common.Source)} <InfoCircleOutlined />
        </Tooltip>
      ),
      dataIndex: 'source',
      render: (value, record) => <Tooltip title={record.url}>{value}</Tooltip>,
    },
    { title: t(Translations.Common.IntervalTime), dataIndex: 'intervalDataReceiveHours' },
    { title: t(Translations.Common.Status), dataIndex: 'isActive', render: (value) => <Status type="icon" status={value} /> },
    {
      render: (value, record) => (
        <Space>
          <PopconfirmDelete itemName={t(Translations.Core.RSS)} pending={rssDeleteApi.pending} data={record.id} onDelete={handleDeleteClicked} />
          <Button
            htmlType="button"
            size="small"
            type="primary"
            icon={<EditOutlined />}
            className="antd-gold6-btn"
            onClick={() => handleEditClicked(record.id)}
          />
        </Space>
      ),
    },
  ];
  table.configColumns(columns);

  return (
    <>
      <AppErrorAlert error={rssGetAllApi.error || rssDeleteApi.error} />
      <Card
        extra={
          <>
            <Button type="primary" htmlType="button" icon={<PlusOutlined />} onClick={handleAddClicked}>
              {t(Translations.Common.Create)}
            </Button>
          </>
        }>
        <Table {...table.tableProps} columns={columns} loading={rssGetAllApi.pending} />
      </Card>
    </>
  );
});

export default RSS;
