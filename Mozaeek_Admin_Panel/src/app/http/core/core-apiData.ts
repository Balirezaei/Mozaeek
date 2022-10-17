import { ApiDataItem } from '../../../types';

export enum CoreApiUrls {
  //Announcement
  AnnouncementGetAllNewsReadyToProcess = '/api/Announcement/GetAllNewsReadyToProcess',
  AnnouncementGetRssNewsById = '/api/Announcement/GetRssNewsById',
  AnnouncementGetById = '/api/Announcement/GetById',
  AnnouncementGetAll = '/api/Announcement/GetAll',
  AnnouncementGetInitDto = '/api/Announcement/GetInitAnnouncementDto',
  AnnouncementCreate = '/api/Announcement/Create',
  AnnouncementDelete = '/api/Announcement/Delete',
  AnnouncementUpdate = '/api/Announcement/Update',
  AnnouncementRssNewsChangeState = '/api/Announcement/RssNewsChangeState',
  AnnouncementGetAllAnnouncementRequest = '/api/Announcement/GetAllAnnouncementRequest',
  AnnouncementGetRequestByRequestTarget = '/api/Announcement/GetRequestByRequestTarget',

  //RSS
  RssGetById = '/api/RSS/GetById',
  RssCreate = '/api/RSS/Create',
  RssGetAllParent = '/api/RSS/GetAllParent',
  RssDelete = '/api/RSS/Delete',
  RssUpdate = '/api/RSS/Update',

  //Label
  LabelGetById = '/api/label/GetById',
  LabelGetAllParent = '/api/label/GetAllParent',
  LabelGetAllChildren = '/api/label/GetAllChildren',
  LabelGetInitDto = '/api/label/GetInitLabelDto',
  LabelCreate = '/api/label/Create',
  LabelDelete = '/api/label/Delete',
  LabelUpdate = '/api/label/Update',
  LabelGetAllSynonyms = '/api/label/GetAllSynonym',
  LabelCreateSynonym = '/api/label/CreateSynonym',
  LabelDeleteSynonym = '/api/label/DeleteSynonym',
  LabelImportFromExcel = '/api/label/ImportFromExcel',

  //RequestAct
  RequestActGetById = '/api/requestAct/GetById',
  RequestActCreate = '/api/requestAct/Create',
  RequestActGetAll = '/api/requestAct/GetAll',
  RequestActDelete = '/api/requestAct/Delete',
  RequestActUpdate = '/api/requestAct/Update',
  RequestActGetAllSynonyms = '/api/requestAct/GetAllSynonym',
  RequestActCreateSynonym = '/api/requestAct/CreateSynonym',
  RequestActDeleteSynonym = '/api/requestAct/DeleteSynonym',

  //Point
  PointGetById = '/api/point/GetById',
  PointGetAllParent = '/api/point/GetAllParent',
  PointGetAllChildren = '/api/point/GetAllChildren',
  PointGetInitDto = '/api/point/GetInitPointDto',
  PointCreate = '/api/point/Create',
  PointDelete = '/api/point/Delete',
  PointUpdate = '/api/point/Update',
  PointImportFromExcel = '/api/point/ImportFromExcel',
  PointDeleteAll = '/api/point/DeletePoints',

  //PreRequest
  PreRequestGetById = '/api/PreRequest/GetById',
  PreRequestCreate = '/api/PreRequest/Create',
  PreRequestGetAll = '/api/PreRequest/GetAll',
  PreRequestDelete = '/api/PreRequest/Delete',
  PreRequestUpdate = '/api/PreRequest/Update',

  //Request
  RequestGetById = '/api/Request/GetById',
  RequestCreate = '/api/Request/Create',
  RequestGetAll = '/api/Request/GetAll',
  RequestGetInitDto = '/api/Request/GetInitRequestDto',
  RequestDelete = '/api/Request/Delete',
  RequestUpdate = '/api/Request/Update',
  RequestSearch = '/api/Request/Search',
  RequestSearchReqestTarget = '/api/request/SearchReqestTarget',
  RequestGetAllNewsRequest = '/api/request/GetAllNewsRequest',
  RequestNewsRequestUpdateRequest = '/api/request/NewsRequestUpdateRequest',
  RequestGetByRequestTarget = '/api/request/GetRequestByRequestTarget',

  //RequestOrg
  RequestOrgGetById = '/api/requestOrg/GetById',
  RequestOrgGetAllParent = '/api/requestOrg/GetAllParent',
  RequestOrgGetAllChildren = '/api/requestOrg/GetAllChildren',
  RequestOrgGetInitDto = '/api/requestOrg/GetInitRequestOrgDto',
  RequestOrgCreate = '/api/requestOrg/Create',
  RequestOrgDelete = '/api/requestOrg/Delete',
  RequestOrgUpdate = '/api/requestOrg/Update',
  RequestOrgGetAllSynonyms = '/api/requestOrg/GetAllSynonym',
  RequestOrgCreateSynonym = '/api/requestOrg/CreateSynonym',
  RequestOrgDeleteSynonym = '/api/requestOrg/DeleteSynonym',
  RequestOrgImportFromExcel = '/api/requestOrg/ImportFromExcel',
  RequestOrgCreateDefinite = '/api/requestOrg/CreateDefiniteRequestOrg',
  RequestOrgRemoveDefinite = '/api/requestOrg/RemoveDefiniteRequestOrg',
  RequestOrgUpdateDefinite = '/api/requestOrg/UpdateDefiniteRequestOrg',
  RequestOrgGetDefiniteById = '/api/requestOrg/GetDefiniteRequestOrdById',
  RequestOrgGetAllDefiniteById = '/api/requestOrg/GetAllDefiniteRequestOrdById',
  RequestOrgGetInitDefinite = '/api/requestOrg/GetInitDefiniteRequestOrg',

  //RequestPrice
  RequestPriceGetById = '/api/RequestPrice/GetById',
  RequestPriceGetAll = '/api/RequestPrice/GetAll',
  RequestPriceGetInitDto = '/api/RequestPrice/GetInitRequestPriceDto',
  RequestPriceCreate = '/api/RequestPrice/Create',
  RequestPriceDelete = '/api/RequestPrice/Delete',
  RequestPriceUpdate = '/api/RequestPrice/Update',

  //Subject
  SubjectGetById = '/api/subject/GetById',
  SubjectGetAllParent = '/api/subject/GetAllParent',
  SubjectGetAllChildren = '/api/subject/GetAllChildren',
  SubjectGetInitDto = '/api/subject/GetInitSubjectDto',
  SubjectCreate = '/api/subject/Create',
  SubjectDelete = '/api/subject/Delete',
  SubjectUpdate = '/api/subject/Update',
  SubjectGetAllSynonyms = '/api/subject/GetAllSynonym',
  SubjectCreateSynonym = '/api/subject/CreateSynonym',
  SubjectDeleteSynonym = '/api/subject/DeleteSynonym',
  SubjectImportFromExcel = '/api/subject/ImportFromExcel',

  //SubjectPrice
  SubjectPriceGetById = '/api/SubjectPrice/GetById',
  SubjectPriceGetAll = '/api/SubjectPrice/GetAll',
  SubjectPriceGetInitDto = '/api/SubjectPrice/GetInitSubjectPriceDto',
  SubjectPriceCreate = '/api/SubjectPrice/Create',
  SubjectPriceDelete = '/api/SubjectPrice/Delete',
  SubjectPriceUpdate = '/api/SubjectPrice/Update',

  //RequestTarget
  RequestTargetGetById = '/api/RequestTarget/GetById',
  RequestTargetGetAll = '/api/RequestTarget/GetAll',
  RequestTargetGetInitDto = '/api/RequestTarget/GetInitRequestTargetDto',
  RequestTargetCreate = '/api/RequestTarget/Create',
  RequestTargetDelete = '/api/RequestTarget/Delete',
  RequestTargetUpdate = '/api/RequestTarget/Update',
}

export const CoreApiDataMap: Map<string, ApiDataItem> = new Map();
