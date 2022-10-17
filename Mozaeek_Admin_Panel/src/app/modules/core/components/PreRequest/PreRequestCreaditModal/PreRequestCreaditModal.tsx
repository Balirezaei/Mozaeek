import { Form, Input, Modal, Spin } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useAntdValidation, useHttpCall } from '../../../../../../features/hooks';
import { Translations } from '../../../../../../features/localization';
import { CreaDitMode } from '../../../../../../types';
import { preRequestCreateHttp, preRequestGetByIdHttp, preRequestUpdateHttp } from '../../../../../http/core/core-http';
import { AppErrorAlert, FormItemActions, FormItemIdHidden, getCreaditSubmitText, getCreaditTitle, successfulResponse } from '../../../../shared';
import { PreRequestItem } from '../../../apiTypes';

type PreRequestFormValues = {
  id: number;
  title: string;
  summary: string;
};

type Props = {
  mode: CreaDitMode;
  visible: boolean;
  onClose: () => void;
  onSuccess: () => void;
  editId?: number;
};
const PreRequestCreaditModal: React.VFC<Props> = React.memo((props) => {
  const { t } = useTranslation();

  const [preRequest, setPreRequest] = useState<PreRequestItem>();

  const preRequestGetByIdApi = useHttpCall(preRequestGetByIdHttp);
  const preRequestCreateApi = useHttpCall(preRequestCreateHttp);
  const preRequestUpdateApi = useHttpCall(preRequestUpdateHttp);

  const [form] = Form.useForm<PreRequestFormValues>();
  const { labelWithRules } = useAntdValidation(form);

  useEffect(() => {
    if (props.editId) {
      preRequestGetByIdApi.call<PreRequestItem>({ id: props.editId }).then((response) => {
        if (successfulResponse(response)) {
          const result = response!.data;
          setPreRequest(result);
        }
      });
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.editId]);

  useEffect(() => {
    if (preRequest) {
      form.setFieldsValue({
        id: preRequest.id,
        title: preRequest.title,
        summary: preRequest.summary,
      });
    } else {
      form.resetFields();
    }
  }, [form, preRequest]);

  const handleSubmit = (values: PreRequestFormValues) => {
    console.log(values);
    if (props.mode === 'Create') {
      preRequestCreateApi.call({ ...values }).then((response) => {
        if (successfulResponse(response)) {
          props.onSuccess();
          handleClose();
        }
      });
    } else {
      preRequestUpdateApi.call({ ...values }).then((response) => {
        if (successfulResponse(response)) {
          props.onSuccess();
          handleClose();
        }
      });
    }
  };

  const handleClose = () => {
    props.onClose();
    setPreRequest(undefined);
    preRequestGetByIdApi.reset();
    preRequestCreateApi.reset();
    preRequestUpdateApi.reset();
    form.resetFields();
  };

  return (
    <Modal
      forceRender
      visible={props.visible}
      onCancel={handleClose}
      footer={null}
      width={600}
      title={
        preRequestGetByIdApi.error === undefined &&
        getCreaditTitle(t, props.mode, t(Translations.Core.PreRequest), preRequest?.title, preRequestGetByIdApi.pending)
      }>
      {preRequestGetByIdApi.error ? (
        <AppErrorAlert error={preRequestGetByIdApi.error} disableAutoHide className="mt-5" />
      ) : (
        <>
          <AppErrorAlert error={preRequestCreateApi.error || preRequestUpdateApi.error} disableAutoHide />
          <Spin spinning={preRequestGetByIdApi.pending}>
            <Form form={form} onFinish={handleSubmit} labelCol={{ span: 4 }}>
              {props.mode === 'Edit' && <FormItemIdHidden id={props.editId} />}
              <Form.Item name="title" {...labelWithRules({ label: t(Translations.Common.Title), rules: [{ type: 'Required' }] })}>
                <Input />
              </Form.Item>
              <Form.Item name="summary" {...labelWithRules({ label: t(Translations.Common.Description), rules: [{ type: 'Required' }] })}>
                <Input.TextArea rows={6} />
              </Form.Item>
              <FormItemActions
                formInstance={form}
                submitPending={preRequestCreateApi.pending || preRequestUpdateApi.pending}
                submitText={getCreaditSubmitText(t, props.mode === 'Create')}
                resetButton={false}
              />
            </Form>
          </Spin>
        </>
      )}
    </Modal>
  );
});

export default PreRequestCreaditModal;
