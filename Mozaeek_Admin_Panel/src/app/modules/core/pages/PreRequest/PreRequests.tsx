import { EditOutlined, PlusOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Card, Space, Table, TableProps } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useImmer } from 'use-immer';

import { useHttpCall, useManualRerender, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { CreaDitMode, GetAllRequestBase } from '../../../../../types';
import { alignTableColumns } from '../../../../../utils/helpers';
import { preRequestDeleteHttp, preRequestGetAllHttp } from '../../../../http/core/core-http';
import { AppErrorAlert, PopconfirmDelete, getPaginationFromAppListResponse, sharedSlice } from '../../../shared';
import { PreRequestGetAllRs, PreRequestItem } from '../../apiTypes';
import PreRequestCreaditModal from '../../components/PreRequest/PreRequestCreaditModal/PreRequestCreaditModal';

const PreRequests: React.VFC = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  const manualRerender = useManualRerender();

  const [request, setRequest] = useState<GetAllRequestBase>({ PageSize: 10, PageNumber: 1 });
  const [preRequestItems, setPreRequestItems] = useState<PreRequestGetAllRs>();
  const [creaditModalData, updateCreaditModalData] = useImmer<{ mode: CreaDitMode; visible: boolean; editId?: number }>({ mode: 'Create', visible: false });

  const preRequestGetAllApi = useHttpCall(preRequestGetAllHttp);
  const preRequestDeleteApi = useHttpCall(preRequestDeleteHttp);

  useMount(() => {
    dispatch(
      sharedSlice.actions.setDisplayPath({
        title: t(Translations.Core.PreRequest),
        breadcrumbs: [{ title: t(Translations.Core.Request) }],
        fontawesomeIcon: 'bullseye',
      })
    );
  });

  useEffect(() => {
    getAll();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [manualRerender.rerenderData, request]);

  const getAll = () => {
    preRequestGetAllApi.call<PreRequestGetAllRs>({ ...request }).then((response) => {
      if (response && !response.error) {
        setPreRequestItems(response.data);
      }
    });
  };

  const handleDeleteClicked = (id: number) => {
    preRequestDeleteApi.call({ id: id }).then((response) => {
      if (response && response.error === null) {
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

  const handlePageChanged = (page: number, pageSize?: number) => {
    setRequest({ PageNumber: page, PageSize: pageSize! });
  };

  const handleCreaditSuccess = () => {
    manualRerender.rerender();
  };

  const columns: TableProps<PreRequestItem>['columns'] = [
    {
      title: t(Translations.Common.Title),
      dataIndex: 'title',
    },
    {
      render: (value, record) => {
        return (
          <Space>
            <PopconfirmDelete
              itemName={t(Translations.Core.PreRequest)}
              pending={preRequestDeleteApi.pending}
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
          </Space>
        );
      },
    },
  ];
  alignTableColumns(columns, 'center');

  return (
    <Card
      title={t(Translations.Core.PreRequests)}
      extra={
        <Space>
          <Button type="primary" htmlType="button" icon={<PlusOutlined />} onClick={handleAddClicked}>
            {t(Translations.Common.Create)}
          </Button>
          <Button type="default" htmlType="button" icon={<ReloadOutlined />} onClick={() => manualRerender.rerender()}>
            {t(Translations.Common.Refresh)}
          </Button>
        </Space>
      }>
      <PreRequestCreaditModal
        mode={creaditModalData.mode}
        visible={creaditModalData.visible}
        editId={creaditModalData.editId}
        onClose={handleCloseModal}
        onSuccess={handleCreaditSuccess}
      />
      <AppErrorAlert error={preRequestGetAllApi.error || preRequestDeleteApi.error} />
      <Table
        rowKey="id"
        loading={preRequestGetAllApi.pending}
        columns={columns}
        dataSource={preRequestItems?.list}
        pagination={getPaginationFromAppListResponse(preRequestItems, handlePageChanged)}
      />
    </Card>
  );
});

export default PreRequests;
