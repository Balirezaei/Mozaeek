import React from 'react';

import { subjectPriceCreateHttp, subjectPriceGetByIdHttp, subjectPriceGetInitDtoHttp, subjectPriceUpdateHttp } from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import PricingCreadit from '../../components/Pricing/PricingCreadit';
import { CorePaths } from '../../CoreRoutes';

type Props = {};
const SubjectPricingCreadit: React.VFC<Props> = React.memo(() => {
  return (
    <PricingCreadit
      apiModule={ApiModule.SubjectPricing}
      http={{
        getById: subjectPriceGetByIdHttp,
        getInitDto: subjectPriceGetInitDtoHttp,
        create: subjectPriceCreateHttp,
        update: subjectPriceUpdateHttp,
      }}
      listPath={CorePaths.SubjectPricing.List}
      icon="money-bill"
    />
  );
});

export default SubjectPricingCreadit;
