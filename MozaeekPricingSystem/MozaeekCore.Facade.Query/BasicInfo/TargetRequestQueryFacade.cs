using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public interface IRequestActQueryFacade
    {
        Task<RequestActDto> GetRequestActById(long id);
        Task<PagedListResult<RequestActDto>> GetAllRequestActs(PagingContract pagingContract);



    }
    public class RequestActQueryFacade : IRequestActQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public RequestActQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }
        
        public Task<RequestActDto> GetRequestActById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, RequestActDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<RequestActDto>> GetAllRequestActs(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<RequestActDto>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, RequestActTotalCount>(new Nothing());
            return new PagedListResult<RequestActDto>()
            {
                List = list,
                // TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

    }
}