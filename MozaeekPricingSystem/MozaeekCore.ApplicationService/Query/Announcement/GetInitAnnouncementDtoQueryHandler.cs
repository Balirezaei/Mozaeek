using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetInitAnnouncementDtoQueryHandler : IBaseAsyncQueryHandler<Nothing, InitAnnouncementDto>
    {
        private readonly IPointQueryService _pointQueryService;
        private readonly IRequestTargetQueryService _requestTargetQueryService;

        public GetInitAnnouncementDtoQueryHandler(IPointQueryService pointQueryService, IRequestTargetQueryService requestTargetQueryService)
        {
            _pointQueryService = pointQueryService;
            _requestTargetQueryService = requestTargetQueryService;
        }

        public async Task<InitAnnouncementDto> HandleAsync(Nothing query)
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

            return new InitAnnouncementDto()
            {
                Points = points,
                RequestTargets = requestTargets
            };
        }
    }
}