import React from 'react';

type Props = {
  icon: React.ReactNode;
  text: string;
  dynamicTextPart?: string;
};
const IconText: React.VFC<Props> = (props) => {
  return (
    <>
      {props.icon}{' '}
      <span>
        {props.text} {props.dynamicTextPart}
      </span>
    </>
  );
};

export default IconText;
