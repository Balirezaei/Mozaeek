using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Enum;

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
        public Task<List<SynonymsDto>> GetAllSynonym()
        {
            return _queryProcessor.ProcessAsync<FindSynonymByEntityType, List<SynonymsDto>>(new FindSynonymByEntityType(EntityType.RequestOrg));
        }

        public Task<DefiniteRequestOrgDto> GetDefiniteRequestOrdById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, DefiniteRequestOrgDto>(new FindByKey(id));
        }
        public Task<List<DefiniteRequestOrgDto>> GetAllDefiniteRequestOrdById(long requestOrgId)
        {
            return _queryProcessor.ProcessAsync<DefiniteRequestOrgContract, List<DefiniteRequestOrgDto>>(new DefiniteRequestOrgContract() { RequestOrgId = requestOrgId });
        }
        public async Task<PagedListResult<DefiniteRequestOrgDto>> GetAllDefiniteRequestOrdByPointId(long pointId)
        {
            var result=await _queryProcessor.ProcessAsync<FindByKey, List<DefiniteRequestOrgDto>>(new FindByKey(pointId));
            return new PagedListResult<DefiniteRequestOrgDto>()
            {
                List = result,
                TotalCount = result.Count,
                PageNumber = 1,
                PageSize = 1
            };
        }

        public Task<InitDefiniteRequestOrg> GetInitDefiniteRequestOrg()
        {
            return _queryProcessor.ProcessAsync<Nothing, InitDefiniteRequestOrg>(new Nothing());
        }

        //IBaseAsyncQueryHandler<FindByKey, DefiniteRequestOrgDto>
        //public class GetAllDefiniteRequestOrgByQueryHandler : IBaseAsyncQueryHandler<DefiniteRequestOrgContract, List<DefiniteRequestOrgDto>>
    }

    public interface IRequestOrgQueryFacade
    {
        Task<RequestOrgDto> GetRequestOrgById(long id);
        Task<PagedListResult<RequestOrgGrid>> GetAllParentRequestOrgs(RequestOrgFilterContract pagingContract);
        Task<PagedListResult<RequestOrgGrid>> GetAllRequestOrgChildren(long parentId);
        Task<InitRequestOrgDto> GetInitRequestOrgDto();
        Task<List<SynonymsDto>> GetAllSynonym();
        Task<DefiniteRequestOrgDto> GetDefiniteRequestOrdById(long id);
        Task<List<DefiniteRequestOrgDto>> GetAllDefiniteRequestOrdById(long requestOrgId);
        Task<PagedListResult<DefiniteRequestOrgDto>> GetAllDefiniteRequestOrdByPointId(long pointId);
        Task<InitDefiniteRequestOrg> GetInitDefiniteRequestOrg();
    }

}