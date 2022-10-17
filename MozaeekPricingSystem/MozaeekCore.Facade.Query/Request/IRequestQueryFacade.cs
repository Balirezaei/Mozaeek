using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public interface IRequestQueryFacade
    {
        Task<RequestDto> GetRequestById(long id);
        Task<PagedListResult<RequestGrid>> GetAllRequests(PagingContract pagingContract);
        Task<InitRequestDto> GetInitRequestDto();
    }

    public class RequestQueryFacade : IRequestQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public RequestQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }
       

        public Task<RequestDto> GetRequestById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, RequestDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<RequestGrid>> GetAllRequests(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<RequestGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, RequestTargetTotalCount>(new Nothing());
            return new PagedListResult<RequestGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }


        public async Task<InitRequestDto> GetInitRequestDto()
        {
            var res = await _queryProcessor.ProcessAsync<Nothing, InitRequestDto>(new Nothing());
            return res;
        }
    }
}