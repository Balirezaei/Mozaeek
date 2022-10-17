import React from 'react';

type Props = {
  icon: React.ReactNode;
  title: string;
};
const TabHeaderWithIcon: React.VFC<Props> = (props) => {
  return (
    <span>
      {props.icon} {props.title}
    </span>
  );
};

export default TabHeaderWithIcon;
