using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetInitDefiniteRequestOrgQueryHandler : IBaseAsyncQueryHandler<Nothing, InitDefiniteRequestOrg>
    {
        private readonly IPointQueryService _pointQueryService;

        public GetInitDefiniteRequestOrgQueryHandler(IPointQueryService pointQueryService)
        {
            _pointQueryService = pointQueryService;
        }
        public async Task<InitDefiniteRequestOrg> HandleAsync(Nothing query)
        {
            var points = await _pointQueryService.GetByPredicate(m => m.ParentId != null);

            return new InitDefiniteRequestOrg()
            {
                Points = points.Select(z => new PointDto()
                {
                    Id = z.Id,
                    Title = z.Title,
                    ParentId = z.ParentId

                }).ToList()
            };
        }
    }
}