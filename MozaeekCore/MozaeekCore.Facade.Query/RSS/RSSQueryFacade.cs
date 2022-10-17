using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public interface IRSSQueryFacade
    {
        Task<RSSDto> GetRSSById(long id);
        Task<PagedListResult<RSSDto>> GetAllRSSs(PagingContract pagingContract);
    }

    public class RSSQueryFacade : IRSSQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public RSSQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<RSSDto> GetRSSById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, RSSDto>(new FindByKey(id));
        }

        public Task<InitRSSDto> GetInitRSSDto()
        {
            return _queryProcessor.ProcessAsync<Nothing, InitRSSDto>(new Nothing());
        }

        public async Task<PagedListResult<RSSDto>> GetAllRSSs(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<RSSDto>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, RSSTotalCount>(new Nothing());
            return new PagedListResult<RSSDto>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

    }
}