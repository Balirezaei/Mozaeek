import React from 'react';

import { Translations } from '../../../../../features/localization';
import { subjectPriceDeleteHttp, subjectPriceGetAllHttp } from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import Pricing from '../../components/Pricing/Pricing';
import { CorePaths } from '../../CoreRoutes';

type Props = {};
const SubjectsPricing: React.VFC<Props> = React.memo(() => {
  return (
    <Pricing
      apiModule={ApiModule.SubjectPricing}
      http={{
        getAll: subjectPriceGetAllHttp,
        delete: subjectPriceDeleteHttp,
      }}
      translationKeys={{
        moduleType: Translations.Core.SubjectPricing,
      }}
      icon="money"
      creaditPath={CorePaths.SubjectPricing.Creadit}
    />
  );
});

export default SubjectsPricing;
