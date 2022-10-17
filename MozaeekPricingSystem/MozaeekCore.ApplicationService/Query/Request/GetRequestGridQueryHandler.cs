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
    public class GetRequestGridQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<RequestGrid>>
    {
        private readonly IRequestQueryService _requestQueryService;

        public GetRequestGridQueryHandler(IRequestQueryService requestQueryService)
        {
            _requestQueryService = requestQueryService;
        }

        public async Task<List<RequestGrid>> HandleAsync(PagingContract query)
        {
            var request =
                await _requestQueryService.GetByPredicate(m => m.Id > 0,
                    new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));
            return request.Select(m => RequestProfile.GetRequestGrid(m)).ToList();
        }
    }


}