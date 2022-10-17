import { Skeleton } from 'antd';
import React, { useMemo } from 'react';

type Props = {
  number: number;
};
const FormSkeleton: React.VFC<Props> = (props) => {
  const skeletons = useMemo(() => {
    return [...Array(props.number)].map((_, index) => <Skeleton active key={index} />);
  }, [props.number]);
  return <>{skeletons}</>;
};

export default FormSkeleton;
