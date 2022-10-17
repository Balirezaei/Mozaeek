import { AppListBaseResponse } from '../../mosaik';

export type RssItem = {
  id: number;
  url: string;
  isActive: boolean;
  source: string;
  intervalDataReceiveHours: number;
};

export type RssGetAllResponse = AppListBaseResponse<RssItem>;

export type RssCreateRequest = {
  url: string;
  isActive: boolean;
  source: string;
  intervalDataReceiveHours: number;
};

export type RssUpdateRequest = {
  id: number;
  url: string;
  isActive: boolean;
  source: string;
};

export type LabelCreateRequest = {
  title: string;
  parentId?: number;
};

export type LabelUpdateRequest = {
  id: number;
  title: string;
  parentId?: number;
};

export type LabelItem = { title: string; id: number; parentId?: number; hasChild?: boolean };

export type LabelGetAllResponse = AppListBaseResponse<LabelItem>;

export type LabelGetInitDtoResponse = { labels: LabelItem[] };

export type RequestActItem = {
  id: number;
  title: string;
};

export type RequestActGetAllResponse = AppListBaseResponse<RequestActItem>;

export type PointItem = {
  id: number;
  parentId: number;
  hasChild: boolean;
  title: string;
};
export type PointGetAllResponse = AppListBaseResponse<PointItem>;

export type SubjectItem = {
  id: number;
  parentId: number;
  hasChild: boolean;
  title: string;
};
export type SubjectGetAllResponse = AppListBaseResponse<SubjectItem>;

export type RequestOrganizationItem = {
  id: number;
  parentId: number;
  hasChild: boolean;
  title: string;
};
export type RequestOrganizationGetAllResponse = AppListBaseResponse<RequestOrganizationItem>;

export type RequestTargetGetInitDtoResponse = {
  labels: LabelItem[];
  requestOrgs: RequestOrganizationItem[];
  subjects: SubjectItem[];
};

export type RequestTargetCreateRequest = {
  title: string;
  subjects: { id: number }[];
  labels?: { id: number }[];
  isDocument: boolean;
};

export type RequestTargetUpdateRequest = RequestTargetCreateRequest & { id: number };

export type RequestTargetItem = {
  id: number;
  title: string;
  labels: string[];
  requestOrgs: string[];
  subjects: string[];
};
export type RequestTargetsGetAllResponse = AppListBaseResponse<RequestTargetItem>;

export type RequestTargetGetByIdResponse = {
  id: number;
  title: string;
  labels: number[];
  requestOrgs: number[];
  subjects: number[];
  isDocument: boolean;
};

export type AnnouncementNewsItem = {
  id: number;
  modifiedDate: Date;
  title: string;
  isProcessed: boolean;
  link: string;
  description: string;
  createDate: Date;
  source: string;
};

export type AnnouncementGetAllNewsReadyToProcessResponse = AppListBaseResponse<AnnouncementNewsItem>;

export type AnnouncementItem = {
  id: number;
  title: string;
  requestTargetTitle: string;
  requestTargetLabels: string[];
  requestTargetRequestOrgs: string[];
  requestTargetSubjects: string[];
  points: string[];
  publishDate: string;
};

export type AnnouncementGetAllResponse = AppListBaseResponse<AnnouncementItem>;

export type AnnouncementGetInitDtoResponse = {
  points: Exclude<PointItem, 'hasChild'>[];
  subjects: Exclude<SubjectItem, 'hasChild'>[];
  labels: Exclude<LabelItem, 'hasChild'>[];
  requestOrgs: Exclude<RequestOrganizationItem, 'hasChild'>[];
};

export type AnnouncementNewsCreateRequest = {
  title: string;
  description: string;
  requestTargetId: number;
  newsId: number;
  points: {
    id: number;
  }[];
};

export type AnnouncementGetByIdResponse = {
  id: number;
  title: string;
  description: string;
  points: number[];
  requestOrgs: RequestOrganizationItem[];
  labels: LabelItem[];
  subjects: SubjectItem[];
  imagePath: string;
  summary: string;
  hasRequest: boolean;
};

export type RequestGetInitDtoResponse = {
  requestTargets: {
    id: number;
    title: string;
  }[];
  requestActs: {
    id: number;
    title: string;
  }[];
  points: PointItem[];
  requestOrgs: Exclude<RequestOrganizationItem, 'hasChild'>[];
  definiteRequestOrgs: {
    id: number;
    title: string;
  }[];
};

export type RequestItem = {
  id: number;
  title: string;
};
export type RequestGetAllResponse = AppListBaseResponse<RequestItem>;

export type RequestQualification = {
  id: number;
  description: string;
  priority: number;
};
export type RequestAction = RequestQualification;
export type RequestNecessity = RequestQualification;
export type RequestDocument = {
  requestTargetId: number;
  requestTargetTitle: string;
  description: string;
};
export type RequestGetByIdResponse = {
  id: number;
  requestTargetId: number;
  requestActId: number;
  requestActions: RequestAction[];
  requestNecessities: RequestNecessity[];
  requestQualifications: RequestQualification[];
  connectionDtos: RequestDocument[];
  points: number[];
  requestExpiredDate: string;
  requestStartDate: string;
  fullOnline: boolean;
  regulation: string;
  summary: string;
};

export type RequestCreateRequest = {
  requestTargetId: number;
  requestActId: number;
  requestActions: {
    id: number;
    description: string;
    priority: number;
  }[];
  requestNecessities: {
    id: number;
    description: string;
    priority: number;
  }[];
  requestQualifications: {
    id: number;
    description: string;
    priority: number;
  }[];
  connectionDtos: {
    requestTargetId: number;
    requestTargetTitle: string;
    description: string;
  }[];
  points: {
    id: number;
  }[];
  requestOrgs: { id: number }[];
  definiteRequestOrgDtos: { id: number }[];
  requestExpiredDate: string;
  requestStartDate: string;
  fullOnline: boolean;
  summary: string;
  regulation: string;
};

export type RequestUpdateRequest = RequestCreateRequest & {
  id: number;
};

export type RequestSearchRequest = {
  title: string;
  excludeRequestIds: number[];
};

export type RequestSearchRequestTargetRq = {
  title: string;
  excludeRequestTargetIds?: number[];
};

export type RequestSearchRequestTargetRs = {
  id: number;
  title: string;
}[];

export type RequestSearchResponse = {
  id: number;
  title: string;
}[];

export type PreRequestCreateRq = {
  title: string;
  summary: string;
};

export type PreRequestUpdateRq = {
  id: number;
} & PreRequestCreateRq;

export type PreRequestItem = {
  id: number;
  title: string;
  summary: string;
  isProcessed: boolean;
};

export type PreRequestGetAllRs = AppListBaseResponse<PreRequestItem>;
