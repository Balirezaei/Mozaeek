using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestOrgsCountQueryHandler : IBaseAsyncQueryHandler<Nothing, RequestOrgTotalCount>
    {
        private readonly IRequestOrgQueryService _requestOrgQueryService;


        public GetRequestOrgsCountQueryHandler(IRequestOrgQueryService requestOrgQueryService)
        {
            _requestOrgQueryService = requestOrgQueryService;
        }

        public async Task<RequestOrgTotalCount> HandleAsync(Nothing query)
        {
            long count = await _requestOrgQueryService.GetCount(m => m.ParentId == null);
            return new RequestOrgTotalCount(count);
        }
    }
}