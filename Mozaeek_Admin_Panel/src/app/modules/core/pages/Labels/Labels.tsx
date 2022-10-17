import React from 'react';

import { Translations } from '../../../../../features/localization';
import {
  labelCreateHttp,
  labelCreateSynonymHttp,
  labelDeleteHttp,
  labelDeleteSynonymHttp,
  labelGetAllChildrenHttp,
  labelGetAllParentsHttp,
  labelGetAllSynonymsHttp,
  labelGetByIdHttp,
  labelGetInitDtoHttp,
  labelImportFromExcelHttp,
  labelUpdateHttp,
} from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import { LabelGetAllResponse, LabelItem } from '../../apiTypes';
import SimpleParentChild from '../SimpleParentChild/SimpleParentChild';

type Props = {};
const Labels: React.VFC<Props> = React.memo(() => {
  return (
    <SimpleParentChild<LabelItem, LabelGetAllResponse>
      apiModule={ApiModule.Labels}
      http={{
        getAllParents: labelGetAllParentsHttp,
        getAllChildren: labelGetAllChildrenHttp,
        getById: labelGetByIdHttp,
        getInitDto: labelGetInitDtoHttp,
        create: labelCreateHttp,
        update: labelUpdateHttp,
        delete: labelDeleteHttp,
        importExcel: labelImportFromExcelHttp,

        getAllSynonyms: labelGetAllSynonymsHttp,
        createSynonym: labelCreateSynonymHttp,
        deleteSynonym: labelDeleteSynonymHttp,
      }}
      translationKeys={{
        item: Translations.Core.Label,
        items: Translations.Core.Labels,
      }}
      icon="tag"
    />
  );
});

export default Labels;
