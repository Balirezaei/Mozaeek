using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.RequestOrg
{


    public class GetAllLevelRequestOrgChildrenQueryHandler : IBaseAsyncQueryHandler<FindByListKey, List<RequestOrgGrid>>
    {
        private readonly IRequestOrgQueryService _requestOrgQueryService;

        public GetAllLevelRequestOrgChildrenQueryHandler(IRequestOrgQueryService requestOrgQueryService)
        {
            _requestOrgQueryService = requestOrgQueryService;
        }

        public async Task<List<RequestOrgGrid>> HandleAsync(FindByListKey query)
        {
            var querys = await _requestOrgQueryService.GetAllLevelChildren(query.Ids);
            return querys.Select(m => new RequestOrgGrid
            {
                Id = m.Id,
                Title = m.Title,
                HasChild = m.HasChild
            }).ToList();
        }
    }
}
