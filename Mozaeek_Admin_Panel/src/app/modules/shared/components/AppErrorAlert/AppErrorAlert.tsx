import { Alert as AntdAlert } from 'antd';
import { AlertProps as AntdAlertProps } from 'antd/lib/alert';
import React, { useEffect, useRef, useState } from 'react';
import { Alert as BootstrapAlert, AlertProps as BootstrapAlertProps } from 'react-bootstrap';

import { AppError } from '../../../../../types';

type BootstrapProps = {
  framework?: 'Bootstrap';
  alertType?: BootstrapAlertProps['variant'];
  error?: AppError;
  showCode?: true;
};

type AntdProps = {
  framework?: 'Antd';
  alertType?: AntdAlertProps['type'];
  error?: AppError;
  showCode?: true;
};
type Props = (BootstrapProps | AntdProps) & { autoHideMillisecond?: number; disableAutoHide?: boolean; className?: string };

const AppErrorAlert: React.VFC<Props> = (props) => {
  const [visible, setVisible] = useState<boolean>(true);

  const timeoutRef = useRef<NodeJS.Timeout>();

  useEffect(() => {
    if (props.error && !props.disableAutoHide) {
      setVisible(true);

      timeoutRef.current = setTimeout(() => {
        setVisible(false);
      }, props.autoHideMillisecond ?? 7000);
    }

    return () => {
      if (timeoutRef.current) {
        clearTimeout(timeoutRef.current);
      }
    };
  }, [props.error, props.disableAutoHide, props.autoHideMillisecond]);

  if (!props.error) {
    return null;
  }

  let alertType = props.alertType;
  if (!alertType) {
    switch (props.framework) {
      case 'Bootstrap':
        alertType = 'danger';
        break;
      case 'Antd':
        alertType = 'error';
        break;
    }
  }
  let errorMessage: string | React.ReactNode = props.error?.message;
  if (errorMessage) {
    let array = undefined;
    if (props.error.message.includes('\r\n')) {
      array = props.error.message.split('\r\n');
    } else if (props.error.message.includes('\n')) {
      array = props.error.message.split('\n');
    }
    if (array) {
      errorMessage = array.map((item, index) => <p key={index}>{item}</p>);
    }
  }

  const message = (
    <>
      {/*{props.showCode && (*/}
      {/*  <>*/}
      {/*    Code : {props.error.code}*/}
      {/*    <br />*/}
      {/*  </>*/}
      {/*)}*/}
      {errorMessage}
      {/*{!props.error.validationErrors && props.error.details && (*/}
      {/*  <>*/}
      {/*    <br />*/}
      {/*    {props.error.details}*/}
      {/*  </>*/}
      {/*)}*/}
      {props.error.validationErrors && (
        <>
          <ul>
            {props.error.validationErrors.map((item) => (
              <li>{item.message}</li>
            ))}
          </ul>
        </>
      )}
    </>
  );

  let alert;

  const handleCloseAlert = () => {
    setVisible(false);
    if (timeoutRef.current) {
      clearTimeout(timeoutRef.current);
    }
  };

  switch (props.framework) {
    case 'Bootstrap':
      alert = (
        <BootstrapAlert className="my-2" variant={alertType as BootstrapAlertProps['variant']} dismissible={!props.disableAutoHide} onClose={handleCloseAlert}>
          {message}
        </BootstrapAlert>
      );
      break;
    case 'Antd':
      alert = <AntdAlert className="my-2" type={alertType as AntdAlertProps['type']} message={message} closable={!props.disableAutoHide} />;
      break;
  }
  return visible ? <div className={props.className}>{alert}</div> : null;
};

AppErrorAlert.defaultProps = {
  framework: 'Bootstrap',
};

export default AppErrorAlert;
