import React from 'react';

import Logo from '../../../../../assets/circleLogo.svg';

type Props = {
  title?: String;
};
const MosaikLoading: React.FC<Props> = (props) => (
  <div className="text-center">
    <div className="logoLoading">
      <img src={Logo} alt={'Mosaik logo'} title={'Mosaik logo'} />
    </div>
    {props.title && (
      <div>
        <strong>{props.title}</strong>
      </div>
    )}
  </div>
);

export default MosaikLoading;
