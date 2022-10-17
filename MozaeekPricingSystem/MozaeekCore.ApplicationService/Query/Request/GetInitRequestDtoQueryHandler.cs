using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetInitRequestDtoQueryHandler : IBaseAsyncQueryHandler<Nothing, InitRequestDto>
    {
        private readonly IPointQueryService _pointQueryService;
        private readonly IRequestTargetQueryService _requestTargetQueryService;

        public GetInitRequestDtoQueryHandler(IPointQueryService pointQueryService, IRequestTargetQueryService requestTargetQueryService)
        {
            _pointQueryService = pointQueryService;
            _requestTargetQueryService = requestTargetQueryService;
        }

        public async Task<InitRequestDto> HandleAsync(Nothing query)
        {
            var points = (await _pointQueryService.GetByPredicate(m => m.Id > 0))
                .Select(m => new PointDto()
                {
                    Id = m.Id,
                    Title = m.Title
                }).ToList();

            var requestTargets = (await _requestTargetQueryService.GetByPredicate(m => m.Id > 0))
                .Select(m => new SmallRequestTargetDto()
                {
                    Id = m.Id,
                    Title = m.Title
                }).ToList();

            return new InitRequestDto()
            {
                Points = points,
                RequestTargets = requestTargets
            };
        }
    }
}