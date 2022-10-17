using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllRequestOrgChildrenQueryHandler : IBaseAsyncQueryHandler<FindByKey, List<RequestOrgGrid>>
    {
        private readonly IRequestOrgQueryService _requestOrgQueryService;

        public GetAllRequestOrgChildrenQueryHandler(IRequestOrgQueryService requestOrgQueryService)
        {
            _requestOrgQueryService = requestOrgQueryService;
        }

        public async Task<List<RequestOrgGrid>> HandleAsync(FindByKey query)
        {
            var querys = await _requestOrgQueryService.GetByPredicate(m => m.ParentId == query.Id);
            return querys.Select(m => new RequestOrgGrid
            {
                Id = m.Id,
                Title = m.Title,
                HasChild = m.HasChild
            }).ToList();
        }
    }
}