import { EyeInvisibleOutlined, EyeTwoTone, KeyOutlined } from '@ant-design/icons';
import { Input } from 'antd';
import { PasswordProps } from 'antd/lib/input/Password';
import React from 'react';

const PasswordInput: React.VFC<PasswordProps> = React.forwardRef((props, ref) => {
  return (
    <Input.Password
      ref={ref}
      prefix={<KeyOutlined />}
      maxLength={16}
      iconRender={(visible) => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
      {...props}
    />
  );
});

export default PasswordInput;
