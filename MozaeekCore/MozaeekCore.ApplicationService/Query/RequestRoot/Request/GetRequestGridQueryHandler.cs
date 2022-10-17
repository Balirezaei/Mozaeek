using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestGridQueryHandler : IBaseAsyncQueryHandler<RequestPagingContract, List<RequestGrid>>
    {
        private readonly IRequestQueryService _requestQueryService;

        public GetRequestGridQueryHandler(IRequestQueryService requestQueryService)
        {
            _requestQueryService = requestQueryService;
        }

        public async Task<List<RequestGrid>> HandleAsync(RequestPagingContract query)
        {
            var request =
                await _requestQueryService.GetByPredicate(new PagingQueryModelContract(query.PageSize, query.PageNumber, query.Sort, query.Order, query.GetSearchParameters()));
            
            
            return request.Select(m => m.GetRequestGrid()).ToList();
        }
    }
}