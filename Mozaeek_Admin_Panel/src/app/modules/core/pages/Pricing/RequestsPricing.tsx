import React from 'react';

import { Translations } from '../../../../../features/localization';
import { requestPriceDeleteHttp, requestPriceGetAllHttp } from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import Pricing from '../../components/Pricing/Pricing';
import { CorePaths } from '../../CoreRoutes';

type Props = {};
const RequestsPricing: React.VFC<Props> = React.memo(() => {
  return (
    <Pricing
      apiModule={ApiModule.RequestPricing}
      http={{
        getAll: requestPriceGetAllHttp,
        delete: requestPriceDeleteHttp,
      }}
      translationKeys={{
        moduleType: Translations.Core.RequestPricing,
      }}
      icon="money-bill"
      creaditPath={CorePaths.RequestPricing.Creadit}
    />
  );
});

export default RequestsPricing;
