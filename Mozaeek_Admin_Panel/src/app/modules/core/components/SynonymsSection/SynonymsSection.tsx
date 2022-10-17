import { Form, Input, Modal, Spin, Table } from 'antd';
import React, { useCallback, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useAntdValidation, useHttpCall } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { ApiResponse } from '../../../../../types';
import { CreateSynonymRq, SynonymItem } from '../../../../http/core/core-apiTypes';
import { AppErrorAlert, PopconfirmDelete, successfulResponse } from '../../../shared';

type SynonymFormValues = {
  text: string;
};
type Props = {
  moduleName: string;
  item: { name: string; time: number } | undefined;
  refreshTime: number;

  http: {
    getAllSynonyms: () => Promise<ApiResponse<SynonymItem[]>>;
    createSynonym: (data: CreateSynonymRq) => Promise<ApiResponse<any>>;
    deleteSynonym: (data: { id: number }) => Promise<ApiResponse<any>>;
  };
};
const SynonymsSection: React.VFC<Props> = React.memo((props: Props) => {
  const { t } = useTranslation();

  const [refreshTime, setRefreshTime] = useState<number>();
  const [synonymModalData, setSynonymModalData] = useState<{ visible: boolean; itemName: string | undefined }>();
  const [data, setData] = useState<SynonymItem[]>();

  const getAllApi = useHttpCall(props.http.getAllSynonyms);
  const createApi = useHttpCall(props.http.createSynonym);
  const deleteApi = useHttpCall(props.http.deleteSynonym);

  const handleDeleteSynonym = useCallback((id: number) => {
    deleteApi.call({ id: id }).then((response) => {
      if (successfulResponse(response)) {
        setRefreshTime(Date.now());
      }
    });

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const [form] = Form.useForm<SynonymFormValues>();
  const { labelWithRules } = useAntdValidation(form);

  const getAllSynonyms = () => {
    getAllApi.call<SynonymItem[]>(undefined).then((response) => {
      if (successfulResponse(response)) {
        setData(response!.data);
      }
    });
  };

  const handleSubmit = (values: SynonymFormValues) => {
    createApi.call({ title: synonymModalData?.itemName!, synonym: values.text }).then((response) => {
      if (successfulResponse(response)) {
        getAllSynonyms();

        setSynonymModalData(undefined);
      }
    });
  };

  useEffect(() => {
    if (refreshTime) {
      getAllSynonyms();
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [refreshTime]);

  useEffect(() => {
    if (props.item?.name) {
      setSynonymModalData({
        itemName: props.item.name,
        visible: true,
      });
    }
  }, [props.item?.name, props.item?.time]);

  useEffect(() => {
    setRefreshTime(props.refreshTime);
  }, [props.refreshTime]);

  return (
    <>
      <Modal
        visible={synonymModalData?.visible}
        title={t(Translations.Core.CreateSynonymForVarWithName, { module: props.moduleName, name: synonymModalData?.itemName })}
        closable
        confirmLoading={createApi.pending}
        onOk={() => form.submit()}
        afterClose={() => {
          form.resetFields();
        }}
        onCancel={() => {
          setSynonymModalData(undefined);
        }}>
        <Spin spinning={createApi.pending}>
          <Form form={form} onFinish={handleSubmit}>
            <Form.Item name="text" {...labelWithRules({ label: t(Translations.Core.Synonym), rules: [{ type: 'Required' }] })}>
              <Input />
            </Form.Item>
          </Form>
        </Spin>
      </Modal>
      {getAllApi.error ? (
        <AppErrorAlert error={getAllApi.error} disableAutoHide />
      ) : (
        <>
          <AppErrorAlert error={deleteApi.error} />
          <Table rowKey="id" dataSource={data} loading={getAllApi.pending}>
            <Table.Column title={t(Translations.Common.Title)} dataIndex="title" align="center" />
            <Table.Column title={t(Translations.Core.Synonym)} dataIndex="synonym" align="center" />
            <Table.Column<SynonymItem>
              align="center"
              render={(value, record) => (
                <PopconfirmDelete
                  itemName={t(Translations.Core.Synonym)}
                  pending={deleteApi.pending}
                  data={record.id}
                  onDelete={() => handleDeleteSynonym(record.id)}
                />
              )}
            />
          </Table>
        </>
      )}
    </>
  );
});

export default SynonymsSection;
