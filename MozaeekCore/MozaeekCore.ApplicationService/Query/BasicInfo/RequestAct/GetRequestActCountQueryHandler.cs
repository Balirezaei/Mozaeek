using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestActCountQueryHandler : IBaseAsyncQueryHandler<Nothing, RequestActTotalCount>
    {
        private readonly IRequestActQueryService _requestActQueryService;

        public GetRequestActCountQueryHandler(IRequestActQueryService requestActQueryService)
        {
            _requestActQueryService = requestActQueryService;
        }

        public async Task<RequestActTotalCount> HandleAsync(Nothing query)
        {
            long count = await _requestActQueryService.GetCount(_ => true);
            return new RequestActTotalCount(count);
        }
    }
}