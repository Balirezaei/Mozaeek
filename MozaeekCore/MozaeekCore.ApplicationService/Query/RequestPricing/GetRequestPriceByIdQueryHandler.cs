using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain.Pricing;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.Persistense.MongoDb.Pricing;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestPriceByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, RequestPriceDto>
    {
        private readonly IRequestPriceQueryService _requestPriceQueryService;
        private readonly IRequestQueryService _requestQueryService;
        public GetRequestPriceByIdQueryHandler( IRequestPriceQueryService requestPriceQueryService)
        {
            _requestPriceQueryService = requestPriceQueryService;
        }
        public async Task<RequestPriceDto> HandleAsync(FindByKey query)
        {
            var res = await _requestPriceQueryService.Get(query.Id);

            return res.GetRequestPriceDto();
        }
    }
}