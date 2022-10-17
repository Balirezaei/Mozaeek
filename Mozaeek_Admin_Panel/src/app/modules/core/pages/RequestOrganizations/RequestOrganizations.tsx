import React, { useCallback } from 'react';
import { useImmer } from 'use-immer';

import { Translations } from '../../../../../features/localization';
import {
  requestOrgCreateHttp,
  requestOrgCreateSynonymHttp,
  requestOrgDeleteHttp,
  requestOrgDeleteSynonymHttp,
  requestOrgGetAllChildrenHttp,
  requestOrgGetAllParentsHttp,
  requestOrgGetAllSynonymsHttp,
  requestOrgGetByIdHttp,
  requestOrgGetInitDtoHttp,
  requestOrgImportFromExcelHttp,
  requestOrgUpdateHttp,
} from '../../../../http/core/core-http';
import { ApiModule } from '../../../apiModule';
import { RequestOrganizationGetAllResponse, RequestOrganizationItem } from '../../apiTypes';
import DefiniteRequestOrganizationModal from '../../components/RequestOrganizations/DefiniteRequestOrganizationModal';
import SimpleParentChild from '../SimpleParentChild/SimpleParentChild';

type Props = {};
const RequestOrganizations: React.VFC<Props> = React.memo(() => {
  const [definiteCreaditModalData, updateDefiniteCreaditModalData] = useImmer<{ requestOrgId?: number; visibility: boolean }>({
    visibility: false,
  });

  const handleOpenModal = useCallback(
    (requestOrgId: number) => {
      updateDefiniteCreaditModalData((draft) => {
        draft.visibility = true;
        draft.requestOrgId = requestOrgId;
      });
    },
    [updateDefiniteCreaditModalData]
  );

  const handleCloseModal = useCallback(() => {
    updateDefiniteCreaditModalData((draft) => {
      draft.visibility = false;
      draft.requestOrgId = undefined;
    });
  }, [updateDefiniteCreaditModalData]);

  return (
    <>
      <DefiniteRequestOrganizationModal
        requestOrgId={definiteCreaditModalData.requestOrgId}
        visibility={definiteCreaditModalData.visibility}
        onClose={handleCloseModal}
      />

      <SimpleParentChild<RequestOrganizationItem, RequestOrganizationGetAllResponse>
        apiModule={ApiModule.RequestOrganizations}
        http={{
          getAllParents: requestOrgGetAllParentsHttp,
          getAllChildren: requestOrgGetAllChildrenHttp,
          getById: requestOrgGetByIdHttp,
          getInitDto: requestOrgGetInitDtoHttp,
          create: requestOrgCreateHttp,
          update: requestOrgUpdateHttp,
          delete: requestOrgDeleteHttp,
          importExcel: requestOrgImportFromExcelHttp,

          getAllSynonyms: requestOrgGetAllSynonymsHttp,
          createSynonym: requestOrgCreateSynonymHttp,
          deleteSynonym: requestOrgDeleteSynonymHttp,
        }}
        translationKeys={{
          item: Translations.Core.RequestOrganization,
          items: Translations.Core.RequestOrganizations,
        }}
        icon="building"
        extra={{
          openDefiniteRequestOrgModal: handleOpenModal,
        }}
      />
    </>
  );
});

export default RequestOrganizations;
