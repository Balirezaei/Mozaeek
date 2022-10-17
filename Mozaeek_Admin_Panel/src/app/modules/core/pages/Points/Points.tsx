import React from 'react';

import { Translations } from '../../../../../features/localization';
import {
  pointCreateHttp,
  pointDeleteAllHttp,
  pointDeleteHttp,
  pointGetAllChildrenHttp,
  pointGetAllParentsHttp,
  pointGetByIdHttp,
  pointGetInitDtoHttp,
  pointImportFromExcelHttp,
  pointUpdateHttp,
} from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import { PointGetAllResponse, PointItem } from '../../apiTypes';
import SimpleParentChild from '../SimpleParentChild/SimpleParentChild';

type Props = {};
const Points: React.VFC<Props> = React.memo(() => {
  return (
    <SimpleParentChild<PointItem, PointGetAllResponse>
      apiModule={ApiModule.Points}
      http={{
        getAllParents: pointGetAllParentsHttp,
        getAllChildren: pointGetAllChildrenHttp,
        getById: pointGetByIdHttp,
        getInitDto: pointGetInitDtoHttp,
        create: pointCreateHttp,
        update: pointUpdateHttp,
        delete: pointDeleteHttp,
        importExcel: pointImportFromExcelHttp,
        deleteAll: pointDeleteAllHttp,
      }}
      translationKeys={{
        item: Translations.Core.Point,
        items: Translations.Core.Points,
      }}
      icon="globe"
    />
  );
});

export default Points;
