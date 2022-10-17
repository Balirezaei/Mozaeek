using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.RequestOrg
{
    public class GetAllDefiniteRequestOrgByQueryHandler : IBaseAsyncQueryHandler<DefiniteRequestOrgContract, List<DefiniteRequestOrgDto>>
    {
        private readonly IDefiniteRequestOrgQueryService _DefiniteRequestOrgQueryService;

        public GetAllDefiniteRequestOrgByQueryHandler(IDefiniteRequestOrgQueryService DefiniteRequestOrgQueryService)
        {
            _DefiniteRequestOrgQueryService = DefiniteRequestOrgQueryService;
        }

        public async Task<List<DefiniteRequestOrgDto>> HandleAsync(DefiniteRequestOrgContract query)
        {
            var querys = await _DefiniteRequestOrgQueryService.GetByPredicate(m=>m.RequestOrg.Id==query.RequestOrgId);
            return querys.Select(m => m.GetDefiniteRequestOrgDto()).ToList();
        }
    }
}