import React from 'react';

type Props = {
  text: string;
};
const OverlayText: React.FC<Props> = React.memo((props) => {
  return (
    <div>
      <div>{props.text}</div>
      <div>{props.children}</div>
    </div>
  );
});

export default OverlayText;
