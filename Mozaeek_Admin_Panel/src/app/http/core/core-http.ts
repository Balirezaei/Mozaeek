import Axios from 'axios';

import { ContentTypes } from '../../../features/constants';
import { AxiosInstanceData, GetAllRequestBase } from '../../../types';
import { urlWithQuery } from '../../../utils/helpers';
import {
  LabelCreateRequest,
  LabelUpdateRequest,
  RequestCreateRequest,
  RequestSearchRequest,
  RequestSearchRequestTargetRq,
  RequestTargetCreateRequest,
  RequestTargetUpdateRequest,
  RequestUpdateRequest,
  RssCreateRequest,
  RssUpdateRequest,
} from '../../modules/core/apiTypes';
import { CoreApiDataMap, CoreApiUrls } from './core-apiData';
import {
  CreateSynonymRq,
  RequestOrgCreateDefiniteRq,
  RequestOrgUpdateDefiniteRq,
  RequestPriceCreateRq,
  RequestPriceUpdateRq,
  SubjectPriceCreateRq,
  SubjectPriceUpdateRq,
} from './core-apiTypes';

const axiosInstance = Axios.create();
axiosInstance.defaults.baseURL = process.env.REACT_APP_CoreApiBaseUrl;

//#region Announcement

export const announcementGetAllNewsReadyToProcessHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.AnnouncementGetAllNewsReadyToProcess, data);
  return axiosInstance.get(url);
};

export const announcementGetNewsByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.AnnouncementGetRssNewsById}/${data.id}`);
};

export const announcementGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.AnnouncementGetById}/${data.id}`);
};

export const announcementGetAllHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.AnnouncementGetAll, data);
  return axiosInstance.get(url);
};

export const announcementGetInitDtoHttp = () => {
  return axiosInstance.get(CoreApiUrls.AnnouncementGetInitDto);
};

export const announcementCreateHttp = (data: FormData) => {
  return axiosInstance.post(CoreApiUrls.AnnouncementCreate, data, {
    headers: {
      'Content-Type': ContentTypes.Multipart,
    },
  });
};

export const announcementUpdateHttp = (data: FormData) => {
  return axiosInstance.post(CoreApiUrls.AnnouncementUpdate, data, {
    headers: {
      'Content-Type': ContentTypes.Multipart,
    },
  });
};

export const announcementDeleteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.AnnouncementDelete, data);
  return axiosInstance.get(url);
};

export const announcementRssNewsChangeStateHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.AnnouncementRssNewsChangeState}/${data.id}`);
};

export const announcementGetAllAnnouncementRequestHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.AnnouncementGetAllAnnouncementRequest, data);
  return axiosInstance.get(url);
};

export const announcementGetRequestByRequestTargetHttp = (data: { RequestTargetId: number }) => {
  const url = urlWithQuery(CoreApiUrls.AnnouncementGetRequestByRequestTarget, data);
  return axiosInstance.get(url);
};

//#endregion

//#region RSS

export const rssGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RssGetById}/${data.id}`);
};

export const rssGetAllParentsHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.RssGetAllParent, data);
  return axiosInstance.get(url);
};

export const rssCreateHttp = (data: RssCreateRequest) => {
  return axiosInstance.post(CoreApiUrls.RssCreate, data);
};

export const rssDeleteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.RssDelete, data);
  return axiosInstance.get(url);
};

export const rssUpdateHttp = (data: RssUpdateRequest) => {
  return axiosInstance.post(CoreApiUrls.RssUpdate, data);
};

//#endregion

//#region Labels

export const labelGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.LabelGetById}/${data.id}`);
};

export const labelGetAllParentsHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.LabelGetAllParent, data);
  return axiosInstance.get(url);
};

export const labelGetAllChildrenHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.LabelGetAllChildren, data);
  return axiosInstance.get(url);
};

export const labelGetInitDtoHttp = () => {
  return axiosInstance.get(CoreApiUrls.LabelGetInitDto);
};

export const labelCreateHttp = (data: LabelCreateRequest) => {
  return axiosInstance.post(CoreApiUrls.LabelCreate, data);
};

export const labelDeleteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.LabelDelete, data);
  return axiosInstance.get(url);
};

export const labelUpdateHttp = (data: LabelUpdateRequest) => {
  return axiosInstance.post(CoreApiUrls.LabelUpdate, data);
};

export const labelGetAllSynonymsHttp = () => {
  return axiosInstance.get(CoreApiUrls.LabelGetAllSynonyms);
};

export const labelCreateSynonymHttp = (data: CreateSynonymRq) => {
  return axiosInstance.post(CoreApiUrls.LabelCreateSynonym, data);
};

export const labelDeleteSynonymHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.LabelDeleteSynonym}/${data.id}`);
};

export const labelImportFromExcelHttp = (data: FormData) => {
  return axiosInstance.post(CoreApiUrls.LabelImportFromExcel, data);
};

//#endregion

//#region RequestAct

export const requestActGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RequestActGetById}/${data.id}`);
};

export const requestActGetAllHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.RequestActGetAll, data);
  return axiosInstance.get(url);
};

export const requestActCreateHttp = (data: { title: string }) => {
  return axiosInstance.post(CoreApiUrls.RequestActCreate, data);
};

export const requestActDeleteHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RequestActDelete}/${data.id}`);
};

export const requestActUpdateHttp = (data: { id: number; title: string }) => {
  return axiosInstance.post(CoreApiUrls.RequestActUpdate, data);
};

export const requestActGetAllSynonymsHttp = () => {
  return axiosInstance.get(CoreApiUrls.RequestActGetAllSynonyms);
};

export const requestActCreateSynonymHttp = (data: CreateSynonymRq) => {
  return axiosInstance.post(CoreApiUrls.RequestActCreateSynonym, data);
};

export const requestActDeleteSynonymHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RequestActDeleteSynonym}/${data.id}`);
};

//#endregion

//#region Points

export const pointGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.PointGetById}/${data.id}`);
};

export const pointGetAllParentsHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.PointGetAllParent, data);
  return axiosInstance.get(url);
};

export const pointGetAllChildrenHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.PointGetAllChildren, data);
  return axiosInstance.get(url);
};

export const pointGetInitDtoHttp = () => {
  return axiosInstance.get(CoreApiUrls.PointGetInitDto);
};

export const pointCreateHttp = (data: LabelCreateRequest) => {
  return axiosInstance.post(CoreApiUrls.PointCreate, data);
};

export const pointDeleteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.PointDelete, data);
  return axiosInstance.get(url);
};

export const pointUpdateHttp = (data: LabelUpdateRequest) => {
  return axiosInstance.post(CoreApiUrls.PointUpdate, data);
};

export const pointImportFromExcelHttp = (data: FormData) => {
  return axiosInstance.post(CoreApiUrls.PointImportFromExcel, data);
};

export const pointDeleteAllHttp = () => {
  return axiosInstance.post(CoreApiUrls.PointDeleteAll);
};

//#endregion

//#region PreRequest

export const preRequestGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.PreRequestGetById}/${data.id}`);
};

export const preRequestGetAllHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.PreRequestGetAll, data);
  return axiosInstance.get(url);
};

export const preRequestCreateHttp = (data: { title: string }) => {
  return axiosInstance.post(CoreApiUrls.PreRequestCreate, data);
};

export const preRequestDeleteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.PreRequestDelete, data);
  return axiosInstance.get(url);
};

export const preRequestUpdateHttp = (data: { id: number; title: string }) => {
  return axiosInstance.post(CoreApiUrls.PreRequestUpdate, data);
};

//#endregion

//#region Requests

export const requestGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RequestGetById}/${data.id}`);
};

export const requestGetAllHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.RequestGetAll, data);
  return axiosInstance.get(url);
};

export const requestGetInitDtoHttp = () => {
  return axiosInstance.get(CoreApiUrls.RequestGetInitDto);
};

export const requestCreateHttp = (data: RequestCreateRequest) => {
  return axiosInstance.post(CoreApiUrls.RequestCreate, data);
};

export const requestDeleteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.RequestDelete, data);
  return axiosInstance.get(url);
};

export const requestUpdateHttp = (data: RequestUpdateRequest) => {
  return axiosInstance.post(CoreApiUrls.RequestUpdate, data);
};

export const requestSearchHttp = (data: RequestSearchRequest) => {
  return axiosInstance.post(CoreApiUrls.RequestSearch, data);
};

export const requestSearchRequestTargetHttp = (data: RequestSearchRequestTargetRq) => {
  return axiosInstance.post(CoreApiUrls.RequestSearchReqestTarget, data);
};

//#endregion

//#region RequestOrg

export const requestOrgGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RequestOrgGetById}/${data.id}`);
};

export const requestOrgGetAllParentsHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.RequestOrgGetAllParent, data);
  return axiosInstance.get(url);
};

export const requestOrgGetAllChildrenHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.RequestOrgGetAllChildren, data);
  return axiosInstance.get(url);
};

export const requestOrgGetInitDtoHttp = () => {
  return axiosInstance.get(CoreApiUrls.RequestOrgGetInitDto);
};

export const requestOrgCreateHttp = (data: LabelCreateRequest) => {
  return axiosInstance.post(CoreApiUrls.RequestOrgCreate, data);
};

export const requestOrgDeleteHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RequestOrgDelete}/${data.id}`);
};

export const requestOrgUpdateHttp = (data: LabelUpdateRequest) => {
  return axiosInstance.post(CoreApiUrls.RequestOrgUpdate, data);
};

export const requestOrgGetAllSynonymsHttp = () => {
  return axiosInstance.get(CoreApiUrls.RequestOrgGetAllSynonyms);
};

export const requestOrgCreateSynonymHttp = (data: CreateSynonymRq) => {
  return axiosInstance.post(CoreApiUrls.RequestOrgCreateSynonym, data);
};

export const requestOrgDeleteSynonymHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RequestOrgDeleteSynonym}/${data.id}`);
};

export const requestOrgImportFromExcelHttp = (data: FormData) => {
  return axiosInstance.post(CoreApiUrls.RequestOrgImportFromExcel, data);
};

export const requestOrgCreateDefiniteHttp = (data: RequestOrgCreateDefiniteRq) => {
  return axiosInstance.post(CoreApiUrls.RequestOrgCreateDefinite, data);
};

export const requestOrgUpdateDefiniteHttp = (data: RequestOrgUpdateDefiniteRq) => {
  return axiosInstance.post(CoreApiUrls.RequestOrgUpdateDefinite, data);
};

export const requestOrgRemoveDefiniteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.RequestOrgRemoveDefinite, data);
  return axiosInstance.get(url);
};

export const requestOrgGetDefiniteByIdHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.RequestOrgGetDefiniteById, data);
  return axiosInstance.get(url);
};

export const requestOrgGetAllDefiniteByIdHttp = (data: { requestOrgId: number }) => {
  const url = urlWithQuery(CoreApiUrls.RequestOrgGetAllDefiniteById, data);
  return axiosInstance.get(url);
};

export const requestOrgGetInitDtoDefiniteHttp = () => {
  return axiosInstance.get(CoreApiUrls.RequestOrgGetInitDefinite);
};

//#endregion

//#region SubjectPrice

export const requestPriceGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RequestPriceGetById}/${data.id}`);
};

export const requestPriceGetAllHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.RequestPriceGetAll, data);
  return axiosInstance.get(url);
};

export const requestPriceGetInitDtoHttp = () => {
  return axiosInstance.get(CoreApiUrls.RequestPriceGetInitDto);
};

export const requestPriceCreateHttp = (data: RequestPriceCreateRq) => {
  return axiosInstance.post(CoreApiUrls.RequestPriceCreate, data);
};

export const requestPriceDeleteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.RequestPriceDelete, data);
  return axiosInstance.get(url);
};

export const requestPriceUpdateHttp = (data: RequestPriceUpdateRq) => {
  return axiosInstance.post(CoreApiUrls.RequestPriceUpdate, data);
};

//#endregion

//#region Subject

export const subjectGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.SubjectGetById}/${data.id}`);
};

export const subjectGetAllParentsHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.SubjectGetAllParent, data);
  return axiosInstance.get(url);
};

export const subjectGetAllChildrenHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.SubjectGetAllChildren, data);
  return axiosInstance.get(url);
};

export const subjectGetInitDtoHttp = () => {
  return axiosInstance.get(CoreApiUrls.SubjectGetInitDto);
};

export const subjectCreateHttp = (data: LabelCreateRequest) => {
  return axiosInstance.post(CoreApiUrls.SubjectCreate, data);
};

export const subjectDeleteHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.SubjectDelete}/${data.id}`);
};

export const subjectUpdateHttp = (data: LabelUpdateRequest) => {
  return axiosInstance.post(CoreApiUrls.SubjectUpdate, data);
};

export const subjectGetAllSynonymsHttp = () => {
  return axiosInstance.get(CoreApiUrls.SubjectGetAllSynonyms);
};

export const subjectCreateSynonymHttp = (data: CreateSynonymRq) => {
  return axiosInstance.post(CoreApiUrls.SubjectCreateSynonym, data);
};

export const subjectDeleteSynonymHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.SubjectDeleteSynonym}/${data.id}`);
};

export const subjectImportFromExcelHttp = (data: FormData) => {
  return axiosInstance.post(CoreApiUrls.SubjectImportFromExcel, data);
};

//#endregion

//#region SubjectPrice

export const subjectPriceGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.SubjectPriceGetById}/${data.id}`);
};

export const subjectPriceGetAllHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.SubjectPriceGetAll, data);
  return axiosInstance.get(url);
};

export const subjectPriceGetInitDtoHttp = () => {
  return axiosInstance.get(CoreApiUrls.SubjectPriceGetInitDto);
};

export const subjectPriceCreateHttp = (data: SubjectPriceCreateRq) => {
  return axiosInstance.post(CoreApiUrls.SubjectPriceCreate, data);
};

export const subjectPriceDeleteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.SubjectPriceDelete, data);
  return axiosInstance.get(url);
};

export const subjectPriceUpdateHttp = (data: SubjectPriceUpdateRq) => {
  return axiosInstance.post(CoreApiUrls.SubjectPriceUpdate, data);
};

//#endregion

//#region RequestTarget

export const requestTargetGetByIdHttp = (data: { id: number }) => {
  return axiosInstance.get(`${CoreApiUrls.RequestTargetGetById}/${data.id}`);
};

export const requestTargetGetAllHttp = (data: GetAllRequestBase) => {
  const url = urlWithQuery(CoreApiUrls.RequestTargetGetAll, data);
  return axiosInstance.get(url);
};

export const requestTargetGetInitDtoHttp = () => {
  return axiosInstance.get(CoreApiUrls.RequestTargetGetInitDto);
};

export const requestTargetCreateHttp = (data: RequestTargetCreateRequest) => {
  return axiosInstance.post(CoreApiUrls.RequestTargetCreate, data);
};

export const requestTargetDeleteHttp = (data: { id: number }) => {
  const url = urlWithQuery(CoreApiUrls.RequestTargetDelete, data);
  return axiosInstance.get(url);
};

export const requestTargetUpdateHttp = (data: RequestTargetUpdateRequest) => {
  return axiosInstance.post(CoreApiUrls.RequestTargetUpdate, data);
};

//#endregion

export const CoreAxiosData: AxiosInstanceData = {
  instance: axiosInstance,
  DataMap: {
    MapObject: CoreApiDataMap,
    Urls: CoreApiUrls,
  },
};
