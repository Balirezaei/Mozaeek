import { AlignCenterOutlined, EditOutlined, PlusOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Card, Modal, Space, Table, TableProps, Tag } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';
import { useImmer } from 'use-immer';

import { useHttpCall, useManualRerender, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { GetAllRequestBase } from '../../../../../types';
import { toQueryString } from '../../../../../utils/helpers';
import { requestTargetDeleteHttp, requestTargetGetAllHttp } from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import { AppErrorAlert, PopconfirmDelete, Status, sharedSlice, successfulResponse, useAntdTable } from '../../../shared';
import { RequestTargetItem, RequestTargetsGetAllResponse } from '../../apiTypes';
import { CorePaths } from '../../CoreRoutes';
import { RequestTargetCreaditQueryString } from '../../types';

type Props = {};
const RequestTargets: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const history = useHistory();

  const manualRerender = useManualRerender();
  const table = useAntdTable<RequestTargetItem>(ApiModule.RequestTargets);

  const [detailsModalData, updateDetailsModalData] = useImmer<{ item?: RequestTargetItem; visible: boolean }>({ visible: false });
  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);

  const getAllApi = useHttpCall(requestTargetGetAllHttp);
  const deleteApi = useHttpCall(requestTargetDeleteHttp);

  useMount(() => {
    dispatch(
      sharedSlice.actions.setDisplayPath({
        title: t(Translations.Core.RequestTargets),
        breadcrumbs: [{ title: t(Translations.Core.Request) }],
        fontawesomeIcon: 'bullseye',
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

  useEffect(() => {
    setRequest(table.requestData);
  }, [table.requestData]);

  const getAll = () => {
    getAllApi.call<RequestTargetsGetAllResponse>({ ...request }).then((response) => {
      if (successfulResponse(response)) {
        table.setData(response!.data);
      }
    });
  };

  const handleAddClicked = () => {
    history.push(CorePaths.RequestTargetCreadit);
  };

  const handleEditClicked = (id: number) => {
    history.push(`${CorePaths.RequestTargetCreadit}?${toQueryString({ id: id } as RequestTargetCreaditQueryString)}`);
  };

  const handleDeleteClicked = (id: number) => {
    deleteApi.call({ id: id }).then((response) => {
      if (successfulResponse(response)) {
        manualRerender.rerender();
      }
    });
  };

  const handleDetailsClicked = (requestTarget: RequestTargetItem) => {
    updateDetailsModalData((draft) => {
      draft.visible = true;
      draft.item = requestTarget;
    });
  };

  const columns: TableProps<RequestTargetItem>['columns'] = [
    { title: t(Translations.Common.Title), dataIndex: 'title' },
    { title: t(Translations.Core.Labels), dataIndex: 'labels', render: (value: string[]) => value?.map((item) => <Tag key={item}>{item}</Tag>) },
    {
      title: t(Translations.Core.Subjects),
      dataIndex: 'subjects',
      render: (value: string[]) => value?.map((item) => <Tag key={item}>{item}</Tag>),
    },
    {
      title: t(Translations.Core.RequestTargetIsDocumentField),
      dataIndex: 'isDocument',
      render: (value: boolean) => <Status status={value} type="icon" />,
    },
    {
      render: (_, record) => (
        <Space>
          <PopconfirmDelete itemName={t(Translations.Core.RequestTarget)} pending={deleteApi.pending} data={record.id} onDelete={handleDeleteClicked} />
          <Button htmlType="button" size="small" type="primary" icon={<EditOutlined />} className="antd-gold6-btn" onClick={() => handleEditClicked(record.id)} />
          <Button type="default" htmlType="button" size="small" icon={<AlignCenterOutlined />} onClick={() => handleDetailsClicked(record)} />
        </Space>
      ),
    },
  ];
  table.configColumns(columns);

  const handleDetailsModalClosed = () => {
    updateDetailsModalData((draft) => {
      draft.visible = false;
      draft.item = undefined;
    });
  };

  return (
    <>
      <Modal
        title={t(Translations.Common.DetailsVar, { item: detailsModalData.item?.title })}
        visible={detailsModalData.visible}
        onCancel={handleDetailsModalClosed}
        footer={null}>
        {detailsModalData.item && (
          <>
            <div className="mb-5">
              <h6>{t(Translations.Core.Labels)} :</h6>
              {detailsModalData.item.labels.map((item) => (
                <Tag key={item}>{item}</Tag>
              ))}
            </div>
            <div className="mb-5">
              <h6>{t(Translations.Core.Subjects)} :</h6>
              {detailsModalData.item.subjects.map((item) => (
                <Tag key={item}>{item}</Tag>
              ))}
            </div>
          </>
        )}
        <Button type="primary" htmlType="button" onClick={handleDetailsModalClosed}>
          {t(Translations.Common.Ok)}
        </Button>
      </Modal>
      <Card
        title={t(Translations.Core.RequestTargets)}
        extra={
          <Space>
            <Button type="primary" htmlType="button" onClick={handleAddClicked} icon={<PlusOutlined />}>
              {t(Translations.Common.Create)}
            </Button>
            <Button type="default" htmlType="button" icon={<ReloadOutlined />} onClick={() => manualRerender.rerender()}>
              {t(Translations.Common.Refresh)}
            </Button>
          </Space>
        }>
        <AppErrorAlert error={getAllApi.error || deleteApi.error} />
        <Table {...table.tableProps} loading={getAllApi.pending} columns={columns} />
      </Card>
    </>
  );
});

export default RequestTargets;
