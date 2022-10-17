import { Form, Input } from 'antd';
import React from 'react';

type Props = { id: number | undefined; inputName?: string };
const FormItemIdHidden: React.VFC<Props> = React.memo((props) => {
  return props.id ? (
    <Form.Item name={props.inputName ?? 'id'} noStyle initialValue={props.id}>
      <Input type="hidden" />
    </Form.Item>
  ) : null;
});

export default FormItemIdHidden;
