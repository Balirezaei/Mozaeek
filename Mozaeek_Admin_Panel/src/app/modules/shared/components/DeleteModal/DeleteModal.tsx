import { Button, Modal } from 'antd';
import React, { useRef } from 'react';
import { useTranslation } from 'react-i18next';

import { Translations } from '../../../../../features/localization';
import { AppError } from '../../../../../types';
import { AppErrorAlert } from '../../index';

interface Props {
  modalContent: React.ReactNode;
  deleteHandler: () => void;
  visible: boolean;
  error: AppError | undefined;
  cancelHandler: () => void;
  deleteButtonLoading: boolean;
}
const DeleteModal: React.VFC<Props> = (props) => {
  const { t } = useTranslation();

  const deleteButton = useRef<HTMLButtonElement>(null);

  const onDeleteClick = () => {
    props.deleteHandler();
    deleteButton.current?.blur();
  };

  return (
    <Modal
      title={t(Translations.Common.Delete)}
      visible={props.visible}
      onCancel={props.cancelHandler}
      footer={[
        <Button ref={deleteButton} key="delete" type="primary" danger onClick={onDeleteClick} loading={props.deleteButtonLoading}>
          {t(Translations.Common.Delete)}
        </Button>,
        <Button key="cancel" type="default" onClick={props.cancelHandler}>
          {t(Translations.Common.Cancel)}
        </Button>,
      ]}>
      <AppErrorAlert error={props.error} framework={'Bootstrap'} />
      {props.modalContent}
    </Modal>
  );
};

export default DeleteModal;
