import { Form, Input, Modal, Spin } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useAntdValidation, useHttpCall } from '../../../../../../features/hooks';
import { Translations } from '../../../../../../features/localization';
import { CreaDitMode } from '../../../../../../types';
import { requestActCreateHttp, requestActGetByIdHttp, requestActUpdateHttp } from '../../../../../http/core/core-http';
import { AppErrorAlert, FormItemActions, FormItemIdHidden, getCreaditSubmitText, getCreaditTitle, successfulResponse } from '../../../../shared';
import { RequestActItem } from '../../../apiTypes';

type RequestActFormValues = {
  id: number;
  title: string;
};

type Props = {
  mode: CreaDitMode;
  visible: boolean;
  onClose: () => void;
  onSuccess: () => void;
  editId?: number;
};
const RequestActCreaditModal: React.VFC<Props> = React.memo((props) => {
  const { t } = useTranslation();

  const [requestAct, setRequestAct] = useState<RequestActItem>();

  const requestActGetByIdApi = useHttpCall(requestActGetByIdHttp);
  const requestActCreateApi = useHttpCall(requestActCreateHttp);
  const requestActUpdateApi = useHttpCall(requestActUpdateHttp);

  const [form] = Form.useForm<RequestActFormValues>();
  const { labelWithRules } = useAntdValidation(form);

  useEffect(() => {
    if (props.editId) {
      requestActGetByIdApi.call<RequestActItem>({ id: props.editId }).then((response) => {
        if (successfulResponse(response)) {
          const result = response!.data;
          setRequestAct(result);
        }
      });
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.editId]);

  useEffect(() => {
    if (requestAct) {
      form.setFieldsValue({
        id: requestAct.id,
        title: requestAct.title,
      });
    } else {
      form.resetFields();
    }
  }, [form, requestAct]);

  const handleSubmit = (values: RequestActFormValues) => {
    if (props.mode === 'Create') {
      requestActCreateApi.call({ ...values }).then((response) => {
        if (successfulResponse(response)) {
          props.onSuccess();
          handleClose();
        }
      });
    } else {
      requestActUpdateApi.call({ ...values }).then((response) => {
        if (successfulResponse(response)) {
          props.onSuccess();
          handleClose();
        }
      });
    }
  };

  const handleClose = () => {
    props.onClose();
    setRequestAct(undefined);
    requestActGetByIdApi.reset();
    requestActCreateApi.reset();
    requestActUpdateApi.reset();
    form.resetFields();
  };

  return (
    <Modal
      forceRender
      visible={props.visible}
      onCancel={handleClose}
      footer={null}
      title={getCreaditTitle(t, props.mode, t(Translations.Core.RequestAct), requestAct?.title, requestActGetByIdApi.pending)}>
      <AppErrorAlert error={requestActGetByIdApi.error || requestActCreateApi.error || requestActUpdateApi.error} />
      <Spin spinning={requestActGetByIdApi.pending}>
        <Form form={form} onFinish={handleSubmit} labelCol={{ span: 4 }}>
          {props.mode === 'Edit' && <FormItemIdHidden id={props.editId} />}
          <Form.Item name="title" {...labelWithRules({ label: t(Translations.Common.Title), rules: [{ type: 'Required' }] })}>
            <Input />
          </Form.Item>
          <FormItemActions
            formInstance={form}
            submitPending={requestActCreateApi.pending || requestActUpdateApi.pending}
            submitText={getCreaditSubmitText(t, props.mode === 'Create')}
            resetButton={false}
          />
        </Form>
      </Spin>
    </Modal>
  );
});

export default RequestActCreaditModal;
