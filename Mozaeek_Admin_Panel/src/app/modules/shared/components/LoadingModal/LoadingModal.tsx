import { LoadingOutlined } from '@ant-design/icons';
import { Modal, Spin } from 'antd';
import React from 'react';
import { useTranslation } from 'react-i18next';

import { Translations } from '../../../../../features/localization';
import { AppError } from '../../../../../types';
import { AppErrorAlert } from '../../index';

type Props = {
  visible: boolean;
  closable: boolean;
  onCancel?: () => void;
  error?: AppError;
  mainMessage?: string;
  showPleaseWaitMessage?: boolean;
};
const LoadingModal: React.VFC<Props> = (props) => {
  const { t } = useTranslation();
  return (
    <Modal centered visible={props.visible} closable={props.closable} footer={null} className="text-center" onCancel={props.onCancel}>
      {props.error ? (
        <div className="my-10">
          <AppErrorAlert error={props.error} />
        </div>
      ) : (
        <div className={'my-10'}>
          <Spin className={'mb-5'} indicator={<LoadingOutlined style={{ fontSize: 40 }} spin />} />
          {props.mainMessage && <div>{props.mainMessage}</div>}
          {props.showPleaseWaitMessage && <div>{t(Translations.Common.PleaseWait)}</div>}
        </div>
      )}
    </Modal>
  );
};

export default LoadingModal;
