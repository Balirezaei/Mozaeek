import { LoadingOutlined } from '@ant-design/icons';
import { Spin } from 'antd';
import React from 'react';

import { ClientFramework } from '../../../../../types';

type Props = {
  active?: boolean;
  framework?: ClientFramework;
};
const Loading: React.FC<Props> = (props) => {
  let element: React.ReactNode;
  switch (props.framework) {
    case 'Bootstrap':
      element = <h1>Not supported!</h1>;
      break;
    case 'Antd':
      element = (
        <Spin spinning={props.active} indicator={<LoadingOutlined style={{ fontSize: 50 }} spin />}>
          {props.children}
        </Spin>
      );
      break;
  }
  return <>{element}</>;
};
Loading.defaultProps = {
  framework: 'Antd',
};

export default Loading;
