using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public interface IAnnouncementQueryFacade
    {
        Task<List<NewsForProcess>> GetAllNewsReadyToProcess(PagingContract contract);
        Task<AnnouncementDto> GetAnnouncementById(long id);
        Task<PagedListResult<AnnouncementGrid>> GetAllAnnouncements(PagingContract pagingContract);
        Task<InitAnnouncementDto> GetInitAnnouncementDto();
    }

    public class AnnouncementQueryFacade : IAnnouncementQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public AnnouncementQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }
        public Task<List<NewsForProcess>> GetAllNewsReadyToProcess(PagingContract contract)
        {
            return _queryProcessor.ProcessAsync<PagingContract, List<NewsForProcess>>(contract);
        }

        public Task<AnnouncementDto> GetAnnouncementById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, AnnouncementDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<AnnouncementGrid>> GetAllAnnouncements(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<AnnouncementGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, RequestTargetTotalCount>(new Nothing());
            return new PagedListResult<AnnouncementGrid>()
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
    }
}