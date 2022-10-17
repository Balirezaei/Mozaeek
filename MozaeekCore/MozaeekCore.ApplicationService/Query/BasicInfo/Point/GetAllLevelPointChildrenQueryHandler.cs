using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace MozaeekCore.ApplicationService.Query.BasicInfo.Point
{
    public class GetAllLevelPointChildrenQueryHandler : IBaseAsyncQueryHandler<FindByListKey, List<AllLevelPointChildren>>
    {
        private readonly IPointQueryService _pointQueryService;

        public GetAllLevelPointChildrenQueryHandler(IPointQueryService pointQueryService)
        {
            _pointQueryService = pointQueryService;
        }

        public async Task<List<AllLevelPointChildren>> HandleAsync(FindByListKey query)
        {
            var querys = await _pointQueryService.GetAllLevelChildren(query.Ids);
            return querys.Select(m => new AllLevelPointChildren
            {
                Id = m.Id,
            }).ToList();
        }
    }
}
