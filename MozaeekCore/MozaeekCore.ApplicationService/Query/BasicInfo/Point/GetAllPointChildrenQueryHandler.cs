using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllPointChildrenQueryHandler : IBaseAsyncQueryHandler<FindByKey, List<PointGrid>>
    {
        private readonly IPointQueryService _pointQueryService;

        public GetAllPointChildrenQueryHandler(IPointQueryService pointQueryService)
        {
            _pointQueryService = pointQueryService;
        }

        public async Task<List<PointGrid>> HandleAsync(FindByKey query)
        {
            var querys = await _pointQueryService.GetByPredicate(m => m.ParentId == query.Id);
            return querys.Select(m => new PointGrid
            {
                Id = m.Id,
                Title = m.Title,
                HasChild = m.HasChild
            }).ToList();
        }
    }
}