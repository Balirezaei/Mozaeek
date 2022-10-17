import { EditOutlined, LinkOutlined, PlusOutlined, ReloadOutlined, TableOutlined } from '@ant-design/icons';
import { Button, Card, Space, Table, TableProps, Tabs } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useImmer } from 'use-immer';

import { useHttpCall, useManualRerender, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { CreaDitMode, GetAllRequestBase } from '../../../../../types';
import {
  requestActCreateSynonymHttp,
  requestActDeleteHttp,
  requestActDeleteSynonymHttp,
  requestActGetAllHttp,
  requestActGetAllSynonymsHttp,
} from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import { AppErrorAlert, PopconfirmDelete, sharedSlice, successfulResponse, useAntdTable } from '../../../shared';
import { RequestActGetAllResponse, RequestActItem } from '../../apiTypes';
import RequestActCreaditModal from '../../components/RequestAct/RequestActCreaditModal/RequestActCreaditModal';
import SynonymsSection from '../../components/SynonymsSection/SynonymsSection';

type Props = {};
const RequestAct: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  const manualRerender = useManualRerender();
  const table = useAntdTable<RequestActItem>(ApiModule.RequestActs);

  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);
  const [creaditModalData, updateCreaditModalData] = useImmer<{ mode: CreaDitMode; visible: boolean; editId?: number }>({ mode: 'Create', visible: false });
  const [tabActiveKey, setTabActiveKey] = useState<string>('table');

  const [synonymItem, setSynonymItem] = useState<{ name: string; time: number }>();
  const [synonymsDataTime, setSynonymsDataTime] = useState<number>(Date.now());

  const requestActGetAllApi = useHttpCall(requestActGetAllHttp);
  const requestActDeleteApi = useHttpCall(requestActDeleteHttp);

  const getAll = () => {
    requestActGetAllApi.call<RequestActGetAllResponse>({ ...request }).then((response) => {
      if (successfulResponse(response)) {
        table.setData(response!.data);
      }
    });
  };

  const handleDeleteClicked = (id: number) => {
    requestActDeleteApi.call({ id: id }).then((response) => {
      if (successfulResponse(response)) {
        manualRerender.rerender();
      }
    });
  };

  const handleEditClicked = (id: number) => {
    updateCreaditModalData((draft) => {
      draft.mode = 'Edit';
      draft.visible = true;
      draft.editId = id;
    });
  };

  const handleAddClicked = () => {
    updateCreaditModalData((draft) => {
      draft.mode = 'Create';
      draft.visible = true;
    });
  };

  const handleCloseModal = () => {
    updateCreaditModalData((draft) => {
      draft.visible = false;
      draft.editId = undefined;
    });
  };

  const handleCreaditSuccess = () => {
    manualRerender.rerender();
  };

  const handleRefreshClicked = () => {
    switch (tabActiveKey) {
      case 'table':
        manualRerender.rerender();
        break;
      case 'synonyms':
        setSynonymsDataTime(Date.now());
        break;
    }
  };

  useMount(() => {
    dispatch(
      sharedSlice.actions.setDisplayPath({
        title: t(Translations.Core.RequestAct),
        breadcrumbs: [{ title: t(Translations.Core.BasicData) }],
        fontawesomeIcon: 'font',
      })
    );
  });

  useEffect(() => {
    getAll();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [manualRerender.rerenderData, request]);

  useEffect(() => {
    setRequest(table.requestData);
  }, [table.requestData]);

  const columns: TableProps<RequestActItem>['columns'] = [
    {
      title: t(Translations.Common.Title),
      dataIndex: 'title',
    },
    {
      width: '20%',
      render: (value, record) => {
        return (
          <Space>
            <PopconfirmDelete
              itemName={t(Translations.Core.RequestAct)}
              pending={requestActDeleteApi.pending}
              data={record.id}
              onDelete={handleDeleteClicked}
            />
            <Button
              htmlType="button"
              size="small"
              type="primary"
              icon={<EditOutlined />}
              className="antd-gold6-btn"
              onClick={() => handleEditClicked(record.id)}
            />
            <Button
              htmlType="button"
              size="small"
              type="default"
              icon={<LinkOutlined />}
              onClick={() => setSynonymItem({ name: record.title, time: Date.now() })}
            />
          </Space>
        );
      },
    },
  ];
  table.configColumns(columns);

  return (
    <Card>
      <Tabs
        tabBarExtraContent={
          <Space>
            {tabActiveKey === 'table' && (
              <Button type="primary" htmlType="button" icon={<PlusOutlined />} onClick={handleAddClicked}>
                {t(Translations.Common.Create)}
              </Button>
            )}
            <Button type="default" htmlType="button" icon={<ReloadOutlined />} onClick={handleRefreshClicked}>
              {t(Translations.Common.Refresh)}
            </Button>
          </Space>
        }
        activeKey={tabActiveKey}
        onChange={setTabActiveKey}>
        <Tabs.TabPane
          key="table"
          tab={
            <span>
              <TableOutlined />
              {t(Translations.Common.TableView)}
            </span>
          }>
          <RequestActCreaditModal
            mode={creaditModalData.mode}
            visible={creaditModalData.visible}
            editId={creaditModalData.editId}
            onClose={handleCloseModal}
            onSuccess={handleCreaditSuccess}
          />
          <AppErrorAlert error={requestActGetAllApi.error} disableAutoHide />
          <AppErrorAlert error={requestActDeleteApi.error} />
          <Table {...table.tableProps} loading={requestActGetAllApi.pending} columns={columns} />
        </Tabs.TabPane>
        <Tabs.TabPane
          forceRender
          key="synonyms"
          tab={
            <span>
              <LinkOutlined />
              {t(Translations.Core.Synonym)}
            </span>
          }>
          <SynonymsSection
            moduleName={t(Translations.Core.RequestAct)}
            item={synonymItem}
            refreshTime={synonymsDataTime}
            http={{
              getAllSynonyms: requestActGetAllSynonymsHttp,
              createSynonym: requestActCreateSynonymHttp,
              deleteSynonym: requestActDeleteSynonymHttp,
            }}
          />
        </Tabs.TabPane>
      </Tabs>
    </Card>
  );
});

export default RequestAct;
