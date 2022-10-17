using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;
using MozaeekCore.ViewModel;
using System.Linq;
using Mozaeek.CR.PublicDto.Dto;

namespace MozaeekCore.Facade.Query
{
    public interface IAnnouncementQueryFacade
    {
        Task<PagedListResult<NewsForProcessGrid>> GetAllNewsReadyToProcess(PagingContract contract);
        Task<NewsForProcess> GetRssNewsById(long id);
        Task<AnnouncementDto> GetAnnouncementById(long id);
        Task<PagedListResult<AnnouncementGrid>> GetAllAnnouncements(PagingContract pagingContract);
        Task<PagedListResult<AnnouncementRequestGrid>> GetAllAnnouncementRequests(PagingContract pagingContract);
        Task<InitAnnouncementDto> GetInitAnnouncementDto();
        Task<PagedListResult<UserAnnouncementDto>> GetUserDashboardAnnouncement(AnnouncementUserDashboardDto announcementUserDashboardDto);
    }

    public class AnnouncementQueryFacade : IAnnouncementQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public AnnouncementQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public async Task<PagedListResult<NewsForProcessGrid>> GetAllNewsReadyToProcess(PagingContract contract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<NewsForProcessGrid>>(contract);
            var count = await _queryProcessor.ProcessAsync<Nothing, NewsForProcessTotalCount>(new Nothing());
            return new PagedListResult<NewsForProcessGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = contract.PageNumber,
                PageSize = contract.PageSize,
            };
        }


        public Task<AnnouncementDto> GetAnnouncementById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, AnnouncementDto>(new FindByKey(id));
        }

        public Task<NewsForProcess> GetRssNewsById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, NewsForProcess>(new FindByKey(id));
        }

        public async Task<PagedListResult<AnnouncementGrid>> GetAllAnnouncements(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<AnnouncementGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, AnnouncementTotalCount>(new Nothing());
            return new PagedListResult<AnnouncementGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

        public async Task<PagedListResult<AnnouncementRequestGrid>> GetAllAnnouncementRequests(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<AnnouncementRequestGrid>>(pagingContract);

            var count = await _queryProcessor.ProcessAsync<Nothing, AnnouncementRequestTotalCount>(new Nothing());
            
            return new PagedListResult<AnnouncementRequestGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }


        public async Task<InitAnnouncementDto> GetInitAnnouncementDto()
        {
            var res = await _queryProcessor.ProcessAsync<Nothing, InitAnnouncementDto>(new Nothing());
            return res;
        }

        public async Task<PagedListResult<UserAnnouncementDto>> GetUserDashboardAnnouncement(AnnouncementUserDashboardDto dto)
        {
            AnnouncementUserDashboardPagingContract pagingContract = new AnnouncementUserDashboardPagingContract()
            {
                PageSize = 100,
                PageNumber = 1,
                SubjectIds = dto.Subjects,
               // LabelIds = dto.Labels,
              //  PointId = dto.PointId,
             //   RequestOrgIds = dto.RequestOrgs,
                UserId = dto.UserId

            };
           // var labelIds = new List<long>();
            var subjectIds = new List<long>();
            //var pointIds = new List<long>();
            //var requestOrgIds = new List<long>();

            //if (dto.Labels != null && dto.Labels.Any())
            //{
            //    var labelList = await _queryProcessor.ProcessAsync<FindByListKey, List<AllLevelLabelChildren>>(new FindByListKey(dto.Labels));
            //    labelIds = (labelList.Select(l => l.Id).ToList());
            //    labelIds.AddRange(dto.Labels);
            //}

            if (dto.Subjects != null && dto.Subjects.Any())
            {

                var subjectList = await _queryProcessor.ProcessAsync<FindByListKey, List<AllLevelSubjectChildren>>(new FindByListKey(dto.Subjects));
                subjectIds = (subjectList.Select(l => l.Id).ToList());
                subjectIds.AddRange(dto.Subjects);
            }

            //if (dto.PointId > 0)
            //{
            //    var pointList = await _queryProcessor.ProcessAsync<FindByListKey, List<AllLevelPointChildren>>(new FindByListKey(new List<long>() { dto.PointId }));
            //    pointIds = (pointList.Select(l => l.Id).ToList());
            //    pointIds.Add(dto.PointId);
            //}

            //if (dto.RequestOrgs != null && dto.RequestOrgs.Any())
            //{

            //    var RequestOrgsList = await _queryProcessor.ProcessAsync<FindByListKey, List<RequestOrgGrid>>(new FindByListKey(dto.RequestOrgs));
            //    requestOrgIds = (RequestOrgsList.Select(l => l.Id).ToList());
            //    requestOrgIds.AddRange(dto.RequestOrgs);
            //}
            var announcementList = (await _queryProcessor.ProcessAsync<AnnouncementUserDashboardPagingContract, List<UserAnnouncementDto>>(new AnnouncementUserDashboardPagingContract() { /*LabelIds = labelIds, PointId = dto.PointId,*/ SubjectIds = subjectIds/*, RequestOrgIds = requestOrgIds*/ }));
            return new PagedListResult<UserAnnouncementDto>()
            {
                List = announcementList,
                TotalCount = announcementList.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }
    }
}