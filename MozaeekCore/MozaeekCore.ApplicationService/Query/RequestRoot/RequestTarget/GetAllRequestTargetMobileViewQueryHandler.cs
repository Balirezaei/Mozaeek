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
    public class GetAllRequestTargetMobileViewQueryHandler : IBaseAsyncQueryHandler<RequestTargetPagingContract, List<RequestTargetMobileView>>
    {
        private readonly IRequestTargetQueryService _requestTargetQueryService;

        public GetAllRequestTargetMobileViewQueryHandler(IRequestTargetQueryService requestTargetQueryService)
        {
            _requestTargetQueryService = requestTargetQueryService;
        }

        public async Task<List<RequestTargetMobileView>> HandleAsync(RequestTargetPagingContract query)
        {
            var querys =
                await _requestTargetQueryService.GetByPredicate(new PagingQueryModelContract(query.PageSize, query.PageNumber, query.Sort, query.Order, query.GetSearchParameters()));

            return querys.Select(m => RequestTargetProfile.GetRequestTargetMobileView(m)).ToList();
        }
    }
}