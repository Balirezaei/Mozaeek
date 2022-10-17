import { Button } from 'antd';
import { ButtonProps } from 'antd/es/button';
import React, { useState } from 'react';

type Props = {
  text: string;
  icon?: ButtonProps['icon'];
  defaultValue?: boolean;
  size?: ButtonProps['size'];
  value?: boolean;
  onChange?: (value: boolean) => void;
};
const CheckableButton: React.VFC<Props> = React.memo((props) => {
  const [status, setStatus] = useState<boolean | undefined>(props.value ?? props.defaultValue);

  const handleButtonClicked = () => {
    if (status === undefined) {
      setStatus(true);
      props.onChange?.(true);
    } else {
      setStatus((prevState) => !prevState);
      props.onChange?.(!status);
    }
  };

  return (
    <>
      <Button type={status ? 'primary' : 'default'} size={props.size} icon={props.icon} onClick={handleButtonClicked}>
        {props.text}
      </Button>
    </>
  );
});

export default CheckableButton;
