import { DeleteRowOutlined } from '@ant-design/icons';
import { Button, ButtonProps, Popconfirm } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { Translations } from '../../../../../features/localization';

type Props = {
  itemName: string;
  pending: boolean;
  data: any;
  onDelete: (data: any) => void;
  title?: string;
  okButtonText?: string;
  buttonProps?: ButtonProps;
};
const PopconfirmDelete: React.VFC<Props> = React.memo((props: Props) => {
  const { t } = useTranslation();

  const [popConfirmVisible, setPopconfirmVisible] = useState<boolean>();

  useEffect(() => {
    if (!props.pending) {
      setPopconfirmVisible(false);
    }
  }, [props.pending]);

  return (
    <Popconfirm
      title={props.title ?? t(Translations.Common.AreYouSureDeleteThisItemVar, { item: props.itemName })}
      okText={props.okButtonText ?? t(Translations.Common.Delete)}
      okButtonProps={{ loading: props.pending, danger: true }}
      onCancel={() => setPopconfirmVisible(false)}
      onConfirm={() => props.onDelete(props.data)}
      visible={popConfirmVisible}>
      <Button
        htmlType="button"
        onClick={() => setPopconfirmVisible(true)}
        title={t(Translations.Common.Delete)}
        icon={<DeleteRowOutlined />}
        type="primary"
        size="small"
        danger
        {...props.buttonProps}
      />
    </Popconfirm>
  );
});

export default PopconfirmDelete;
