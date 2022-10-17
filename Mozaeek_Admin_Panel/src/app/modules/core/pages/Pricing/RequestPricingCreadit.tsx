import React from 'react';

import { requestPriceCreateHttp, requestPriceGetByIdHttp, requestPriceGetInitDtoHttp, requestPriceUpdateHttp } from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import PricingCreadit from '../../components/Pricing/PricingCreadit';
import { CorePaths } from '../../CoreRoutes';

type Props = {};
const RequestPricingCreadit: React.VFC<Props> = React.memo(() => {
  return (
    <PricingCreadit
      apiModule={ApiModule.RequestPricing}
      http={{
        getById: requestPriceGetByIdHttp,
        getInitDto: requestPriceGetInitDtoHttp,
        create: requestPriceCreateHttp,
        update: requestPriceUpdateHttp,
      }}
      listPath={CorePaths.RequestPricing.List}
      icon="money-bill"
    />
  );
});

export default RequestPricingCreadit;
