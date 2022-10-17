import { EditOutlined } from '@ant-design/icons';
import { Button } from 'antd';
import React from 'react';

type Props = {
  onClick?: () => void;
};
const EditTableActionButton: React.VFC<Props> = React.memo((props: Props) => {
  return <Button htmlType="button" size="small" type="primary" icon={<EditOutlined />} className="antd-gold6-btn" onClick={props.onClick} />;
});

export default EditTableActionButton;
