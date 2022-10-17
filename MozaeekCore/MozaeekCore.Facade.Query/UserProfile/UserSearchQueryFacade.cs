using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.ApplicationService.Contract.BasicInfo.RequestTarget;
using MozaeekCore.ApplicationService.Contract.UserProfile;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Mapper.Announcement;
using MozaeekCore.ViewModel;

namespace MozaeekCore.Facade.Query.UserProfile
{
    public interface IUserSearchQueryFacade
    {
        Task<SingleUserAnnouncementDto> GetUserAnnouncementById(long announcementId);
        Task<SingleUserRequestDto> GetUserRequestByRequestTargetId(long requestTargetId);
        Task<SingleUserRequestDto> GetUserRequestByRequestTargetId(long requestTargetId, long? requestActId, long pointId);
        Task<UserSearchResult> FullTextSearch(FullTextSearchDto input);
        Task<UserSearchResult> FullUserSearchBySubject(FullUserSearchBySubject input);
        Task<UserSearchResult> FullUserSearchByRequestTarget(FullUserSearchByRequestTarget input);
        Task<UserSearchResult> FullUserSearchByRequestOrg(FullUserSearchByRequestOrg input);
        Task<UserSearchResult> FullSearchByUserCharacteristics(FullUserSearchByUserCharacteristics input);
    }

    public class UserSearchQueryFacade : IUserSearchQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;
        public UserSearchQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public async Task<UserSearchResult> FullTextSearch(FullTextSearchDto input)
        {
            var requestTargetMobileResult = new List<RequestTargetMobileView>();
            var userAnnoucementResult = new List<UserAnnouncementDto>();
            var labels = (await _queryProcessor.ProcessAsync<FindByTextSearch, List<LabelGrid>>(new FindByTextSearch(input.Query))).Select(l => l.Id).ToList();
            var subjects = (await _queryProcessor.ProcessAsync<FindByTextSearch, List<SubjectGrid>>(new FindByTextSearch(input.Query))).Select(l => l.Id).ToList();
            var requestOrgs = (await _queryProcessor.ProcessAsync<FindByTextSearch, List<RequestOrgGrid>>(new FindByTextSearch(input.Query))).Select(l => l.Id).ToList();
            var points = (await _queryProcessor.ProcessAsync<FindByTextSearch, List<PointGrid>>(new FindByTextSearch(input.Query))).Select(l => l.Id).ToList();
            var annoucements = await _queryProcessor.ProcessAsync<FindByTextSearch, List<UserAnnouncementDto>>(new FindByTextSearch(input.Query));
            var requestTargets = await _queryProcessor.ProcessAsync<FindByTextSearch, List<RequestTargetGrid>>(new FindByTextSearch(input.Query));
            requestTargetMobileResult.AddRange(await _queryProcessor.ProcessAsync<UserSearchByBasicInfoQuery, List<RequestTargetMobileView>>(new UserSearchByBasicInfoQuery(labels, subjects, requestOrgs, 1)));
            requestTargetMobileResult.AddRange(requestTargets?.Select(r => new RequestTargetMobileView() { Id = r.Id, Title = r.Title }));
            userAnnoucementResult.AddRange(await _queryProcessor.ProcessAsync<FindByListKey, List<UserAnnouncementDto>>(new FindByListKey(points)));
            userAnnoucementResult.AddRange(await _queryProcessor.ProcessAsync<FindByListKey, List<UserAnnouncementDto>>(new FindByListKey(requestTargetMobileResult.Select(r => r.Id).ToList())));
            userAnnoucementResult.AddRange(annoucements);
            userAnnoucementResult = userAnnoucementResult.GroupBy(r => r.Id).Select(r => r.First()).ToList();
            requestTargetMobileResult = requestTargetMobileResult.GroupBy(r => r.Id).Select(r => r.First()).ToList();
            return new UserSearchResult()
            {
                Announcments = new Core.ResponseMessages.PagedListResult<UserAnnouncementDto>() { List = userAnnoucementResult, PageSize = 1, PageNumber = 1, TotalCount = annoucements.Count },
                RequestTargets = new Core.ResponseMessages.PagedListResult<RequestTargetMobileView>() { List = requestTargetMobileResult, PageSize = 1, PageNumber = 1, TotalCount = requestTargets.Count }
            };
        }

        public async Task<UserSearchResult> FullUserSearchBySubject(FullUserSearchBySubject input)
        {
            var subjectIds = new List<long>() { input.SubjectId };
            var children = (await _queryProcessor.ProcessAsync<FindByListKey, List<AllLevelSubjectChildren>>(new FindByListKey(subjectIds))).Select(l => l.Id).ToList();
            subjectIds.AddRange(children);

            var announcements = (await _queryProcessor.ProcessAsync<AnnouncementUserDashboardPagingContract, List<UserAnnouncementDto>>(
                new AnnouncementUserDashboardPagingContract() { SubjectIds = subjectIds }));

            var requestTargets = await _queryProcessor.ProcessAsync<UserDashboardSearchBySubjectQuery, List<RequestTargetMobileView>>(
                new UserDashboardSearchBySubjectQuery(subjectIds.ToArray(), 1));

            return new UserSearchResult()
            {
                Announcments = new Core.ResponseMessages.PagedListResult<UserAnnouncementDto>() { List = announcements, PageSize = 10, PageNumber = 1, TotalCount = 1 },
                RequestTargets = new Core.ResponseMessages.PagedListResult<RequestTargetMobileView>() { List = requestTargets, PageSize = 10, PageNumber = 1, TotalCount = 1 }
            };
        }

        public async Task<UserSearchResult> FullUserSearchByRequestTarget(FullUserSearchByRequestTarget input)
        {
            var announcements =
                await _queryProcessor.ProcessAsync<UserSearchByRequestTargetQuery, List<UserAnnouncementDto>>(
                    new UserSearchByRequestTargetQuery(input.RequestTargetId, 1));

            var requestTargets = await _queryProcessor.ProcessAsync<UserSearchByRequestTargetQuery, List<RequestTargetMobileView>>(
                new UserSearchByRequestTargetQuery(input.RequestTargetId, 1));

            return new UserSearchResult()
            {
                Announcments = new Core.ResponseMessages.PagedListResult<UserAnnouncementDto>() { List = announcements, PageSize = 10, PageNumber = 1, TotalCount = 1 },
                RequestTargets = new Core.ResponseMessages.PagedListResult<RequestTargetMobileView>() { List = requestTargets, PageSize = 10, PageNumber = 1, TotalCount = 1 }
            };
        }

        public async Task<UserSearchResult> FullUserSearchByRequestOrg(FullUserSearchByRequestOrg input)
        {
            var announcements =
                await _queryProcessor.ProcessAsync<UserSearchByRequestOrgQuery, List<UserAnnouncementDto>>(
                    new UserSearchByRequestOrgQuery(input.RequestOrgId, 1));

            var requestTargets = await _queryProcessor.ProcessAsync<UserSearchByRequestOrgQuery, List<RequestTargetMobileView>>(
                new UserSearchByRequestOrgQuery(input.RequestOrgId, 1));

            return new UserSearchResult()
            {
                Announcments = new Core.ResponseMessages.PagedListResult<UserAnnouncementDto>() { List = announcements, PageSize = 10, PageNumber = 1, TotalCount = 1 },
                RequestTargets = new Core.ResponseMessages.PagedListResult<RequestTargetMobileView>() { List = requestTargets, PageSize = 10, PageNumber = 1, TotalCount = 1 }
            };
        }

        public async Task<UserSearchResult> FullSearchByUserCharacteristics(FullUserSearchByUserCharacteristics input)
        {
            var children = (await _queryProcessor.ProcessAsync<FindByListKey, List<AllLevelLabelChildren>>(new FindByListKey(input.LabelIds))).Select(l => l.Id).ToList();
            input.LabelIds.AddRange(children);

            var announcements = (await _queryProcessor
                .ProcessAsync<UserDashboardSearchByCharactresticQuery, List<UserAnnouncementDto>>(
                    new UserDashboardSearchByCharactresticQuery(input.LabelIds, 1)));

            var requestTargets = await _queryProcessor.ProcessAsync<UserDashboardSearchByCharactresticQuery, List<RequestTargetMobileView>>(
                new UserDashboardSearchByCharactresticQuery(input.LabelIds, 1));

            return new UserSearchResult()
            {
                Announcments = new Core.ResponseMessages.PagedListResult<UserAnnouncementDto>() { List = announcements, PageSize = 10, PageNumber = 1, TotalCount = 1 },
                RequestTargets = new Core.ResponseMessages.PagedListResult<RequestTargetMobileView>() { List = requestTargets, PageSize = 10, PageNumber = 1, TotalCount = 1 }
            };

        }

        public async Task<SingleUserAnnouncementDto> GetUserAnnouncementById(long announcementId)
        {
            var announcementDto = await _queryProcessor.ProcessAsync<FindByKey, AnnouncementDto>(new FindByKey(announcementId));
            var singleUserAnnouncement = announcementDto.GetSingleUserAnnouncement();
            return singleUserAnnouncement;
        }

        public async Task<SingleUserRequestDto> GetUserRequestByRequestTargetId(long requestTargetId)
        {
            var condidateRequest =
               (await _queryProcessor.ProcessAsync<RequestPagingContract, List<RequestListGroupByRequestTarget>>(
                    new RequestPagingContract()
                    {
                        PageSize = 5,
                        PageNumber = 1,
                        RequestTargetId = requestTargetId
                    })).FirstOrDefault();


            var userRequest = condidateRequest.GetSingleUserRequest();

            var properPrice = await _queryProcessor.ProcessAsync<ProperPriceForRequestQuestionQuery, ProperPriceRequestQuestion>(new ProperPriceForRequestQuestionQuery(condidateRequest.Id));
            if (properPrice != null)
            {
                userRequest.PriceAmount = properPrice.ProperPriceResult.UnitPrice;
            }
            else
            {
                userRequest.PriceAmount = 0;
            }
            //TODO : Should Imp after Technician Microservice
            userRequest.TechnicianCount = 0;

            return userRequest;
        }

        public async Task<SingleUserRequestDto> GetUserRequestByRequestTargetId(long requestTargetId, long? requestActId, long pointId)
        {
            var condidateRequest =
                (await _queryProcessor.ProcessAsync<RequestPagingContract, List<RequestListGroupByRequestTarget>>(
                    new RequestPagingContract()
                    {
                        PageSize = 1,
                        PageNumber = 1,
                        RequestTargetId = requestTargetId,
                        RequestActId = requestActId,
                        PointId = pointId
                    })).FirstOrDefault();

            if (condidateRequest==null)
            {
                return null;
            }
            var userRequest = condidateRequest.GetSingleUserRequest();
            var properPrice = await _queryProcessor.ProcessAsync<ProperPriceForRequestQuestionQuery, ProperPriceRequestQuestion>(new ProperPriceForRequestQuestionQuery(condidateRequest.Id));
            if (properPrice != null)
            {
                userRequest.PriceAmount = properPrice.ProperPriceResult.UnitPrice;
            }
            else
            {
                userRequest.PriceAmount = 0;
            }
            //TODO : Should Imp after Technician Microservice
            userRequest.TechnicianCount = 0;

            return userRequest;
        }
    }
}