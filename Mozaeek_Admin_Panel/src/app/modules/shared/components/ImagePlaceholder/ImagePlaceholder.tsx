import clsx from 'clsx';
import React, { useState } from 'react';

import defaultInitialImageSrc from '../../../../../assets/image.svg';
import Classes from './ImagePlaceholder.module.scss';

type Props = {
  imageSrc: string;
  initialImageSrc?: string;
  imageAlt?: string;
  className?: string;
};
const ImagePlaceholder: React.VFC<Props> = React.memo((props) => {
  const [loaded, setLoaded] = useState(false);

  const handleLoad = () => {
    setLoaded(true);
  };

  return (
    <div className={Classes.ImagePlaceHolderWrapper + ' ' + props.className || ''}>
      <img src={props.imageSrc} alt={props.imageAlt || ''} onLoad={handleLoad} />
      <img
        src={props.initialImageSrc || defaultInitialImageSrc}
        className={clsx({ [Classes.ImagePlaceHolderInitial]: true, [Classes.Disappear]: loaded })}
        alt={props.imageAlt || ''}
      />
    </div>
  );
});

export default ImagePlaceholder;
