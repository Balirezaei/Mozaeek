import { EditOutlined } from '@ant-design/icons';
import { Button, Form, Input, Modal, Space, Spin, Table, TreeSelect } from 'antd';
import { ColumnsType } from 'antd/es/table';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useAntdValidation, useHttpCall, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { CreaDitMode } from '../../../../../types';
import { alignTableColumns, filterTreeNodeByString, getTreeData } from '../../../../../utils/helpers';
import { RequestOrgDefiniteGetByIdRs, RequestOrgDefiniteGetInitDtoRs, RequestOrgDefiniteItem, RequestOrgGetAllDefiniteByIdRs } from '../../../../http/core/core-apiTypes';
import {
  requestOrgCreateDefiniteHttp,
  requestOrgGetAllDefiniteByIdHttp,
  requestOrgGetDefiniteByIdHttp,
  requestOrgGetInitDtoDefiniteHttp,
  requestOrgRemoveDefiniteHttp,
  requestOrgUpdateDefiniteHttp,
} from '../../../../http/core/core-http';
import { AppErrorAlert, FormItemActions, PopconfirmDelete, successfulResponse } from '../../../shared';

type DefiniteRequestOrgCreaditFormValues = {
  pointId: number;
  address: string;
  phoneNumber: string;
};

type Props = {
  requestOrgId?: number;
  visibility: boolean;
  onClose: () => void;
};
const DefiniteRequestOrganizationModal: React.VFC<Props> = React.memo((props: Props) => {
  const { t } = useTranslation();

  const [definiteInitDto, setDefiniteInitDto] = useState<RequestOrgDefiniteGetInitDtoRs>();
  const [allDefiniteRequestOrganizations, setAllDefiniteRequestOrganizations] = useState<RequestOrgGetAllDefiniteByIdRs>();
  const [creadit, setCreadit] = useState<{ mode: CreaDitMode; editId?: number }>({ mode: 'Create' });

  const definiteGetInitDtoApi = useHttpCall(requestOrgGetInitDtoDefiniteHttp);
  const definiteCreateApi = useHttpCall(requestOrgCreateDefiniteHttp);
  const definiteUpdateApi = useHttpCall(requestOrgUpdateDefiniteHttp);
  const definiteRemoveApi = useHttpCall(requestOrgRemoveDefiniteHttp);
  const definiteGetByIdApi = useHttpCall(requestOrgGetDefiniteByIdHttp);
  const definiteGetAllByIdApi = useHttpCall(requestOrgGetAllDefiniteByIdHttp);

  const [form] = Form.useForm<DefiniteRequestOrgCreaditFormValues>();
  const { labelWithRules } = useAntdValidation(form);

  useMount(() => {
    definiteGetInitDtoApi.call<RequestOrgDefiniteGetInitDtoRs>(undefined).then((response) => {
      if (successfulResponse(response)) {
        setDefiniteInitDto(response!.data);
      }
    });
  });

  useEffect(() => {
    if (!props.visibility) {
      form.resetFields();
      setCreadit({
        editId: undefined,
        mode: 'Create',
      });

      definiteCreateApi.reset();
      definiteUpdateApi.reset();
      definiteRemoveApi.reset();
      definiteGetByIdApi.reset();
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.visibility, form]);

  useEffect(() => {
    if (props.requestOrgId) {
      getAll();
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.requestOrgId]);

  const getAll = () => {
    definiteGetAllByIdApi.call<RequestOrgGetAllDefiniteByIdRs>({ requestOrgId: props.requestOrgId! }).then((response) => {
      if (successfulResponse(response)) {
        setAllDefiniteRequestOrganizations(response!.data);
      }
    });
  };

  const handleDefiniteModalCancelClicked = () => {
    props.onClose();
    setAllDefiniteRequestOrganizations(undefined);
  };

  const handleDefiniteCreaditSubmit = (values: DefiniteRequestOrgCreaditFormValues) => {
    if (creadit.mode === 'Create') {
      definiteCreateApi
        .call({
          requestOrgId: props.requestOrgId!,
          pointId: values.pointId,
          address: values.address,
          phoneNumber: values.phoneNumber,
        })
        .then((response) => {
          if (successfulResponse(response)) {
            getAll();
            resetForm();
          }
        });
    } else {
      definiteUpdateApi
        .call({
          id: creadit.editId!,
          pointId: values.pointId,
          address: values.address,
          phoneNumber: values.phoneNumber,
        })
        .then((response) => {
          if (successfulResponse(response)) {
            getAll();
            resetForm();
          }
        });
    }
  };

  const handleEditClicked = (id: number) => {
    definiteGetByIdApi.call<RequestOrgDefiniteGetByIdRs>({ id: id }).then((response) => {
      if (successfulResponse(response)) {
        setCreadit({
          editId: id,
          mode: 'Edit',
        });
        const result = response!.data;
        form.setFieldsValue({
          address: result.address,
          phoneNumber: result.phoneNumber,
          pointId: result.point.id,
        });
      }
    });
  };

  const handleDeleteClicked = (id: number) => {
    definiteRemoveApi.call({ id: id }).then((response) => {
      if (successfulResponse(response)) {
        getAll();
      }
    });
  };

  const handleResetClicked = () => {
    resetForm();
  };

  const resetForm = () => {
    form.resetFields();
    setCreadit({
      editId: undefined,
      mode: 'Create',
    });
  };

  const columns: ColumnsType<RequestOrgDefiniteItem> = [
    {
      title: t(Translations.Core.Point),
      dataIndex: ['point', 'title'],
    },
    {
      title: t(Translations.Common.PhoneNumber),
      dataIndex: 'phoneNumber',
    },
    {
      title: t(Translations.Common.Address),
      dataIndex: 'address',
    },
    {
      dataIndex: 'id',
      render: (value) => (
        <Space>
          <Button htmlType="button" size="small" type="primary" icon={<EditOutlined />} className="antd-gold6-btn" onClick={() => handleEditClicked(value)} />
          <PopconfirmDelete itemName={t(Translations.Core.DefiniteRequestOrganization)} pending={definiteRemoveApi.pending} data={value} onDelete={handleDeleteClicked} />
        </Space>
      ),
    },
  ];
  alignTableColumns(columns, 'center');

  return (
    <Modal title={t(Translations.Core.DefiniteRequestOrganization)} visible={props.visibility} closable onCancel={handleDefiniteModalCancelClicked} footer={null}>
      {definiteGetInitDtoApi.error ? (
        <AppErrorAlert error={definiteGetInitDtoApi.error} />
      ) : (
        <>
          <AppErrorAlert error={definiteGetByIdApi.error || definiteCreateApi.error || definiteUpdateApi.error || definiteRemoveApi.error} />
          <Spin spinning={definiteGetInitDtoApi.pending || definiteCreateApi.pending || definiteUpdateApi.pending || definiteGetByIdApi.pending}>
            <Form form={form} labelCol={{ span: 6 }} onFinish={handleDefiniteCreaditSubmit}>
              <Form.Item name="pointId" {...labelWithRules({ label: t(Translations.Core.Point), rules: [{ type: 'Required' }] })}>
                <TreeSelect treeData={getTreeData(definiteInitDto?.points, 'title', 'id')} filterTreeNode={filterTreeNodeByString} showSearch allowClear />
              </Form.Item>
              <Form.Item name="phoneNumber" {...labelWithRules({ label: t(Translations.Common.Phone), rules: [{ type: 'Required' }] })}>
                <Input />
              </Form.Item>
              <Form.Item name="address" className="mb-1" {...labelWithRules({ label: t(Translations.Common.Address), rules: [{ type: 'Required' }] })}>
                <Input />
              </Form.Item>
              <FormItemActions formInstance={form} creaditMode={creadit.mode} onReset={handleResetClicked} />
            </Form>
          </Spin>
        </>
      )}
      <hr />
      <AppErrorAlert error={definiteGetAllByIdApi.error} />
      <Table loading={definiteGetAllByIdApi.pending} dataSource={allDefiniteRequestOrganizations} columns={columns} size="small" pagination={{ pageSize: 5 }} />
    </Modal>
  );
});

export default DefiniteRequestOrganizationModal;
