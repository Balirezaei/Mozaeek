import React from 'react';

import { useAppSelector } from '../../../../../features/hooks';

function Error() {
  useAppSelector((state) => state.shared.httpError);
  return <div>A</div>;
}

export default Error;
