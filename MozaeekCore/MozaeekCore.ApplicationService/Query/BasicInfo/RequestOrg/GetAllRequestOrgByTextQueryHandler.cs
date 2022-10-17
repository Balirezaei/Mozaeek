using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.RequestOrg
{

    public class GetAllRequestOrgByTextQueryHandler : IBaseAsyncQueryHandler<FindByTextSearch, List<RequestOrgGrid>>
    {
        private readonly IRequestOrgQueryService _requestOrgQueryService;

        public GetAllRequestOrgByTextQueryHandler(IRequestOrgQueryService requestOrgQueryService)
        {
            _requestOrgQueryService = requestOrgQueryService;
        }

        public async Task<List<RequestOrgGrid>> HandleAsync(FindByTextSearch query)
        {
            var querys = await _requestOrgQueryService.GetAllByText(query.Query);
            return querys.Select(m => m.GetRequestOrgGrid()).ToList();
        }
    }
}
