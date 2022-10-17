using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllPointQueryHandler : IBaseAsyncQueryHandler<PointFilterContract, List<PointGrid>>
    {
        private readonly IPointQueryService _labelQueryService;

        public GetAllPointQueryHandler(IPointQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<List<PointGrid>> HandleAsync(PointFilterContract query)
        {
            var querys =
                await _labelQueryService.GetByPredicate(new PagingQueryModelContract(query.PageSize, query.PageNumber, query.Sort, query.Order, query.GetSearchParameters()));

            return querys.Select(m => new PointGrid
            {
                Id = m.Id,
                Title = m.Title,
                HasChild = m.HasChild
            }).ToList();
        }
    }
}