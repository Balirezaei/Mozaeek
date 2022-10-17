import React from 'react';

type Props = {
  text: string;
};
const TableColumnTitle: React.VFC<Props> = (props) => {
  return <>{props.text.toUpperCase()}</>;
};

export default TableColumnTitle;
