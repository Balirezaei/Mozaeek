using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public interface IRequestTargetQueryFacade
    {
        Task<RequestTargetDto> GetRequestTargetById(long id);
        Task<PagedListResult<RequestTargetGrid>> GetAllRequestTargets(PagingContract pagingContract);

        Task<InitRequestTargetDto> GetInitRequestTargetDto(long? id);

    }

    public class RequestTargetQueryFacade : IRequestTargetQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public RequestTargetQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<RequestTargetDto> GetRequestTargetById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, RequestTargetDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<RequestTargetGrid>> GetAllRequestTargets(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<RequestTargetGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, RequestTargetTotalCount>(new Nothing());
            return new PagedListResult<RequestTargetGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

        public async Task<InitRequestTargetDto> GetInitRequestTargetDto(long? id)
        {
            var res = await _queryProcessor.ProcessAsync<FindByKeyEditMode, InitRequestTargetDto>(new FindByKeyEditMode(null));
            return res;
        }
    }
}