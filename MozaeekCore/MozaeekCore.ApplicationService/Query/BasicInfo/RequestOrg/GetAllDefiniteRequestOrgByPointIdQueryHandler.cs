using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.RequestOrg
{
    public class GetAllDefiniteRequestOrgByPointIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, List<DefiniteRequestOrgDto>>
    {
        private readonly IDefiniteRequestOrgQueryService _DefiniteRequestOrgQueryService;

        public GetAllDefiniteRequestOrgByPointIdQueryHandler(IDefiniteRequestOrgQueryService DefiniteRequestOrgQueryService)
        {
            _DefiniteRequestOrgQueryService = DefiniteRequestOrgQueryService;
        }

        public async Task<List<DefiniteRequestOrgDto>> HandleAsync(FindByKey query)
        {
            var querys = await _DefiniteRequestOrgQueryService.GetByPredicate(m => m.Point.Id == query.Id);
            return querys.Select(m => m.GetDefiniteRequestOrgDto()).ToList();
        }
    }
}
