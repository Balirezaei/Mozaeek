using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestTotalCountQueryHandler : IBaseAsyncQueryHandler<RequestPagingContract, RequestTotalCount>
    {
        private readonly IRequestQueryService _requestQueryService;

        public GetRequestTotalCountQueryHandler(IRequestQueryService requestQueryService)
        {
            _requestQueryService = requestQueryService;
        }

        public async Task<RequestTotalCount> HandleAsync(RequestPagingContract query)
        {
            long count = await _requestQueryService.GetCount(query.GetSearchParameters());
            return new RequestTotalCount(count);
        }
    }
}