using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestTargetCountQueryHandler : IBaseAsyncQueryHandler<RequestTargetPagingContract, RequestTargetTotalCount>
    {
        private readonly IRequestTargetQueryService _requestTargetQueryService;

        public GetRequestTargetCountQueryHandler(IRequestTargetQueryService requestTargetQueryService)
        {
            _requestTargetQueryService = requestTargetQueryService;
        }

        public async Task<RequestTargetTotalCount> HandleAsync(RequestTargetPagingContract query)
        {
            long count = await _requestTargetQueryService.GetCount(query.GetSearchParameters());
            return new RequestTargetTotalCount(count);
        }
    }

}