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
        private readonly IRequestActQueryService _requestActQueryService;
        private readonly IRequestOrgQueryService _requestOrgQueryService;
        private readonly IDefiniteRequestOrgQueryService _definiteRequestOrgQueryService;
        public GetInitRequestDtoQueryHandler(IPointQueryService pointQueryService, IRequestTargetQueryService requestTargetQueryService, IRequestActQueryService requestActQueryService, IRequestOrgQueryService requestOrgQueryService, IDefiniteRequestOrgQueryService definiteRequestOrgQueryService)
        {
            _pointQueryService = pointQueryService;
            _requestTargetQueryService = requestTargetQueryService;
            _requestActQueryService = requestActQueryService;
            _requestOrgQueryService = requestOrgQueryService;
            _definiteRequestOrgQueryService = definiteRequestOrgQueryService;
        }

        public async Task<InitRequestDto> HandleAsync(Nothing query)
        {
            var points = (await _pointQueryService.GetByPredicate(_ => true))
                .Select(m => new PointDto()
                {
                    Id = m.Id,
                    Title = m.Title,
                    ParentId = m.ParentId
                }).ToList();

            var requestOrg = (await _requestOrgQueryService.GetByPredicate(_ => true))
                .Select(m => new RequestOrgDto()
                {
                    Id = m.Id,
                    Title = m.Title,
                    ParentId = m.ParentId
                }).ToList();

            var definiteRequestOrg = (await _definiteRequestOrgQueryService.GetByPredicate(_ => true))
                .Select(m => new SelectDefiniteRequestOrgDto()
                {
                    Id = m.Id,
                    Title = m.GetTitle(),
                }).ToList();

            var requestTargets = (await _requestTargetQueryService.GetByPredicate(_ => true))
                .Select(m => new SmallRequestTargetDto()
                {
                    Id = m.Id,
                    Title = m.Title
                }).ToList();

            var requestActs = (await _requestActQueryService.GetByPredicate(_ => true)).Select(m =>
                new RequestActDto()
                {
                    Id = m.Id,
                    Title = m.Title
                }).ToList();

            return new InitRequestDto()
            {
                Points = points,
                RequestTargets = requestTargets,
                RequestActs = requestActs,
                RequestOrgs = requestOrg,
                DefiniteRequestOrgs = definiteRequestOrg
            };
        }
    }
}