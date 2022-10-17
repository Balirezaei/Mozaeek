using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.RequestOrg
{
    public class GetDefiniteRequestOrgByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, DefiniteRequestOrgDto>
    {
        private readonly IDefiniteRequestOrgQueryService _DefiniteRequestOrgQueryService;

        public GetDefiniteRequestOrgByIdQueryHandler(IDefiniteRequestOrgQueryService DefiniteRequestOrgQueryService)
        {
            _DefiniteRequestOrgQueryService = DefiniteRequestOrgQueryService;
        }

        public async Task<DefiniteRequestOrgDto> HandleAsync(FindByKey query)
        {
            var DefiniteRequestOrgQuery = await _DefiniteRequestOrgQueryService.Get(query.Id);
            return DefiniteRequestOrgQuery.GetDefiniteRequestOrgDto();
        }
    }
}