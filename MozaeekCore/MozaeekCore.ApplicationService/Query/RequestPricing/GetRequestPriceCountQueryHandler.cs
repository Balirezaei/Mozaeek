using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb.Pricing;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestPriceCountQueryHandler : IBaseAsyncQueryHandler<Nothing, RequestPriceTotalCount>
    {
        private readonly IRequestPriceQueryService _requestTargetQueryService;

        public GetRequestPriceCountQueryHandler(IRequestPriceQueryService requestTargetQueryService)
        {
            _requestTargetQueryService = requestTargetQueryService;
        }

        public async Task<RequestPriceTotalCount> HandleAsync(Nothing query)
        {
            long count = await _requestTargetQueryService.GetCount(_ => true);
            return new RequestPriceTotalCount(count);
        }

    }

}