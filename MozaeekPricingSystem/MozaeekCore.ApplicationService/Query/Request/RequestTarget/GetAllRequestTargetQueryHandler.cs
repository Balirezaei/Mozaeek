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
    public class GetAllRequestTargetQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<RequestTargetGrid>>
    {
        private readonly IRequestTargetQueryService _requestTargetQueryService;

        public GetAllRequestTargetQueryHandler(IRequestTargetQueryService requestTargetQueryService)
        {
            _requestTargetQueryService = requestTargetQueryService;
        }


        public async Task<List<RequestTargetGrid>> HandleAsync(PagingContract query)
        {
            var querys =
                await _requestTargetQueryService.GetByPredicate(m => m.Id > 0,
                    new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));

            return querys.Select(m => m.GetRequestTargetGrid()).ToList();
        }
    }
}