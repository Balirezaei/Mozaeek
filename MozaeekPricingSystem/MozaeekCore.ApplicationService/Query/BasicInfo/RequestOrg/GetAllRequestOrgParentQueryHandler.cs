using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllRequestOrgParentQueryHandler : IBaseAsyncQueryHandler<RequestOrgFilterContract, List<RequestOrgGrid>>
    {
        private readonly IRequestOrgQueryService _requestOrgQueryService;

        public GetAllRequestOrgParentQueryHandler(IRequestOrgQueryService requestOrgQueryService)
        {
            _requestOrgQueryService = requestOrgQueryService;
        }

        public async Task<List<RequestOrgGrid>> HandleAsync(RequestOrgFilterContract query)
        {
            var querys =
                await _requestOrgQueryService.GetByPredicate(m => m.ParentId == null, new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));

            return querys.Select(m => new RequestOrgGrid
            {
                Id = m.Id,
                Title = m.Title,
                HasChild = m.HasChild
            }).ToList();
        }
    }
}