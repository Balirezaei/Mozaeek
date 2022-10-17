import React, { useEffect } from 'react';
import { useLocation } from 'react-router-dom';

import { scrollTop } from '../../../../../utils/helpers';

const App: React.FC = (props) => {
  const location = useLocation();

  useEffect(() => {
    scrollTop();
  }, [location.pathname]);

  return <>{props.children}</>;
};

export default App;
