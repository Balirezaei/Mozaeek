using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.Point
{
    public class GetAllPointByTextQueryHandler : IBaseAsyncQueryHandler<FindByTextSearch, List<PointGrid>>
    {
        private readonly IPointQueryService _pointQueryService;

        public GetAllPointByTextQueryHandler(IPointQueryService pointQueryService)
        {
            _pointQueryService = pointQueryService;
        }

        public async Task<List<PointGrid>> HandleAsync(FindByTextSearch query)
        {
            var querys = await _pointQueryService.GetAllByText(query.Query);
            return querys.Select(m => m.GetPointGrid()).ToList();
        }
    }

}
