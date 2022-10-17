using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public class PreRequestQueryFacade : IPreRequestQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public PreRequestQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }
        
        public Task<PreRequestDto> GetPreRequestById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, PreRequestDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<PreRequestGrid>> GetAllPreRequests(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<PreRequestGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, PreRequestTotalCount>(new Nothing());
            return new PagedListResult<PreRequestGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }
    }
}