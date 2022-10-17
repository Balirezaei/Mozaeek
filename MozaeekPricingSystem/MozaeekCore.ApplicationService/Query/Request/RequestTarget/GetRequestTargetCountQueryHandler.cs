using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestTargetCountQueryHandler : IBaseAsyncQueryHandler<Nothing, RequestTargetTotalCount>
    {
        private readonly IRequestTargetQueryService _requestTargetQueryService;

        public GetRequestTargetCountQueryHandler(IRequestTargetQueryService requestTargetQueryService)
        {
            _requestTargetQueryService = requestTargetQueryService;
        }

        public async Task<RequestTargetTotalCount> HandleAsync(Nothing query)
        {
            long count = await _requestTargetQueryService.GetCount(m => m.Id>0);
            return new RequestTargetTotalCount(count);
        }
    }

}