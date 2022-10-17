using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;

namespace MozaeekCore.Facade.Query
{
    public class RequestOrgQueryFacade : IRequestOrgQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public RequestOrgQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<RequestOrgDto> GetRequestOrgById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, RequestOrgDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<RequestOrgGrid>> GetAllRequestOrgChildren(long parentId)
        {
            var list = await _queryProcessor.ProcessAsync<FindByKey, List<RequestOrgGrid>>(new FindByKey(parentId));
            return new PagedListResult<RequestOrgGrid>()
            {
                List = list,
                TotalCount = list.Count,
                PageNumber = 1,
                PageSize = 1
            };
        }

        public async Task<PagedListResult<RequestOrgGrid>> GetAllParentRequestOrgs(RequestOrgFilterContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<RequestOrgFilterContract, List<RequestOrgGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, RequestOrgTotalCount>(new Nothing());
            return new PagedListResult<RequestOrgGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

        public Task<InitRequestOrgDto> GetInitRequestOrgDto()
        {
            return _queryProcessor.ProcessAsync<Nothing, InitRequestOrgDto>(new Nothing());
        }
    }

    public interface IRequestOrgQueryFacade
    {
        Task<RequestOrgDto> GetRequestOrgById(long id);
        Task<PagedListResult<RequestOrgGrid>> GetAllParentRequestOrgs(RequestOrgFilterContract pagingContract);
        Task<PagedListResult<RequestOrgGrid>> GetAllRequestOrgChildren(long parentId);
        Task<InitRequestOrgDto> GetInitRequestOrgDto();
    }

}