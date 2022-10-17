using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetPointsCountQueryHandler : IBaseAsyncQueryHandler<Nothing, PointTotalCount>
    {
        private readonly IPointQueryService _labelQueryService;


        public GetPointsCountQueryHandler(IPointQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<PointTotalCount> HandleAsync(Nothing query)
        {
            long count = await _labelQueryService.GetCount(m => m.ParentId == null);
            return new PointTotalCount(count);
        }
    }
}