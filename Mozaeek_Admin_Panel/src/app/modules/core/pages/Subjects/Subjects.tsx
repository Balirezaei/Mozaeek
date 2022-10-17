import React from 'react';

import { Translations } from '../../../../../features/localization';
import {
  subjectCreateHttp,
  subjectCreateSynonymHttp,
  subjectDeleteHttp,
  subjectDeleteSynonymHttp,
  subjectGetAllChildrenHttp,
  subjectGetAllParentsHttp,
  subjectGetAllSynonymsHttp,
  subjectGetByIdHttp,
  subjectGetInitDtoHttp,
  subjectImportFromExcelHttp,
  subjectUpdateHttp,
} from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import { SubjectGetAllResponse, SubjectItem } from '../../apiTypes';
import SimpleParentChild from '../SimpleParentChild/SimpleParentChild';

type Props = {};
const Subjects: React.VFC<Props> = React.memo(() => {
  return (
    <SimpleParentChild<SubjectItem, SubjectGetAllResponse>
      apiModule={ApiModule.Subjects}
      http={{
        getAllParents: subjectGetAllParentsHttp,
        getAllChildren: subjectGetAllChildrenHttp,
        getById: subjectGetByIdHttp,
        getInitDto: subjectGetInitDtoHttp,
        create: subjectCreateHttp,
        update: subjectUpdateHttp,
        delete: subjectDeleteHttp,
        importExcel: subjectImportFromExcelHttp,

        getAllSynonyms: subjectGetAllSynonymsHttp,
        createSynonym: subjectCreateSynonymHttp,
        deleteSynonym: subjectDeleteSynonymHttp,
      }}
      translationKeys={{
        item: Translations.Core.Subject,
        items: Translations.Core.Subjects,
      }}
      icon="sticky-note"
    />
  );
});

export default Subjects;
