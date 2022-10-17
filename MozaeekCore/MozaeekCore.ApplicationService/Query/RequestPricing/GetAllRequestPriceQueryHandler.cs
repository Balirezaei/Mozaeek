using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb.Pricing;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllRequestPriceQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<RequestPriceGrid>>
    {
        private readonly IRequestPriceQueryService _requestTargetQueryService;

        public GetAllRequestPriceQueryHandler(IRequestPriceQueryService requestTargetQueryService)
        {
            _requestTargetQueryService = requestTargetQueryService;
        }
        
        public async Task<List<RequestPriceGrid>> HandleAsync(PagingContract query)
        {
            var querys =
                await _requestTargetQueryService.GetByPredicate(_ => true,
                    new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));

            return querys.Select(m => m.GetRequestPriceGrid()).ToList();
        }
    }
}