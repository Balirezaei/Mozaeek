import { ClearOutlined, LoadingOutlined, SendOutlined } from '@ant-design/icons';
import { Button, Space } from 'antd';
import { FormInstance } from 'antd/es/form';
import React from 'react';
import { useTranslation } from 'react-i18next';

import { Translations } from '../../../../../features/localization';
import { CreaDitMode } from '../../../../../types';

type Props = {
  formInstance: FormInstance;
  submitPending?: boolean;
  submitPendingIconOnly?: boolean;
  resetButton?: boolean;
  onReset?: () => void;
  submitIcon?: React.ReactNode;
  submitText?: string;
  submitDisabled?: boolean;
  resetText?: string;
  resetIcon?: React.ReactNode;
  creaditMode?: CreaDitMode;
};

const FormItemActions: React.VFC<Props> = React.memo((props) => {
  const { t } = useTranslation();

  const handleReset = () => {
    if (props.onReset) {
      props.onReset();
    } else {
      props.formInstance.resetFields();
    }
  };
  return (
    <>
      <Space>
        <Button
          className="form-action-Btn"
          type="primary"
          htmlType="submit"
          icon={props.submitPending ? <LoadingOutlined /> : props.submitIcon !== undefined ? props.submitIcon : <SendOutlined className="mirror-in-rtl" />}
          loading={!props.submitPendingIconOnly && props.submitPending}
          disabled={props.submitDisabled}>
          {props.submitText
            ? props.submitText
            : props.creaditMode
            ? props.creaditMode === 'Create'
              ? t(Translations.Common.Creation)
              : t(Translations.Common.Edition)
            : t(Translations.Common.Submit)}
        </Button>
        {props.resetButton && !props.submitPending && (
          <Button
            className="form-action-Btn"
            type="default"
            htmlType="button"
            icon={props.resetIcon ? props.resetIcon : <ClearOutlined />}
            onClick={() => handleReset()}>
            {props.resetText ? props.resetText : t(Translations.Common.ResetForm)}
          </Button>
        )}
      </Space>
    </>
  );
});
FormItemActions.defaultProps = {
  resetButton: true,
};

export default FormItemActions;
