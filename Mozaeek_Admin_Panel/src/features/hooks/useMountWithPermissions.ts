import { EffectCallback } from 'react';
import { useHistory } from 'react-router-dom';

import { useMount, usePermissions } from './index';

const useMountWithPermissions = (effect: EffectCallback | undefined, permissions: readonly string[]) => {
  const history = useHistory();

  const { hasPermissions } = usePermissions();

  useMount(() => {
    if (hasPermissions(permissions)) {
      if (effect) {
        effect();
      }
    } else {
      history.push('/unauthorized');
    }
  });
};

export default useMountWithPermissions;
