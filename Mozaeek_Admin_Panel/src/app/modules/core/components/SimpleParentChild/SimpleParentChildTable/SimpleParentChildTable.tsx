import React from 'react';

type Props = {};
const SimpleParentChildTable: React.VFC<Props> = React.memo((props: Props) => {
  return (
    <div>
      <p>A</p>
    </div>
  );
});

export default SimpleParentChildTable;
