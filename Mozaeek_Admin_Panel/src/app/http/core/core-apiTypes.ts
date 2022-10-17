import { PointItem, RequestItem, SubjectItem } from '../../modules/core/apiTypes';
import { AppListBaseResponse } from '../../mosaik';

export type CreateSynonymRq = {
  title: string;
  synonym: string;
};

export type SynonymItem = {
  id: number;
  title: string;
  synonym: string;
  entityType: number;
  entityDescription: string;
};

export type UnitPrice = {
  id: number;
  title: string;
};

export type PriceItem = {
  id: number;
  title: string;
  startDate: string;
  isActive: boolean;
};

export type PricingGetByIdRs = {
  id: number;
  priceUnitId: number;
  priceAmount: number;
  title: string;
  startDate: string;
  endDate: string;
  isActive: boolean;
  systemShare: number;
  technicianShare: number;
};

export type PricingCreateRq = {
  title: string;
  startDate: string;
  endDate: string;
  priceAmount: number;
  priceUnitId: number;
  systemShare: number;
};

export type PricingGetInitDto = {
  unitPrices: UnitPrice[];
};

export type SubjectPriceGetByIdRs = PricingGetByIdRs & {
  priceDetails: {
    subjectId: number;
    subjectTitle: string;
  }[];
};

export type SubjectPriceItem = PriceItem;

export type SubjectPriceGetAllRs = AppListBaseResponse<SubjectPriceItem>;

export type SubjectPriceGetInitDtoRs = PricingGetInitDto & {
  subjectList: SubjectItem[];
};

export type SubjectPriceCreateRq = PricingCreateRq & { subjectIds: number[] };

export type SubjectPriceUpdateRq = {
  id: number;
} & SubjectPriceCreateRq;

export type RequestPriceGetByIdRs = PricingGetByIdRs & {
  priceDetails: {
    requestId: number;
    requestTitle: string;
  }[];
};

export type RequestPriceCreateRq = PricingCreateRq & { requestIds: number[] };
export type RequestPriceUpdateRq = {
  id: number;
} & RequestPriceCreateRq;

export type RequestPriceGetInitDtoRs = PricingGetInitDto & {
  requestList: RequestItem[];
};

export type AnnouncementRequestItem = {
  id: number;
  title: string;
  requestTargetTitle: string;
  requestTargetLabels: string[];
  requestTargetRequestOrgs: string[];
  requestTargetSubjects: string[];
  points: string[];
  publishDate: string;
};

export type AnnouncementRequestsGetAllRs = AppListBaseResponse<AnnouncementRequestItem>;

export type RequestOrgCreateDefiniteRq = {
  requestOrgId: number;
  pointId: number;
  address: string;
  phoneNumber: string;
};

export type RequestOrgUpdateDefiniteRq = {
  id: number;
  pointId: number;
  address: string;
  phoneNumber: string;
};

export type RequestOrgDefiniteGetInitDtoRs = {
  points: PointItem[];
};

export type RequestOrgDefiniteItem = {
  id: number;
  point: PointItem;
  address: string;
  phoneNumber: string;
};

export type RequestOrgDefiniteGetByIdRs = RequestOrgDefiniteItem;

export type RequestOrgGetAllDefiniteByIdRs = RequestOrgDefiniteGetByIdRs[];
