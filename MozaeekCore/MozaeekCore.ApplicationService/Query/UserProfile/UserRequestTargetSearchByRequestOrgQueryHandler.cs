using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.UserProfile;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query.UserProfile
{
    public class UserRequestTargetSearchByRequestOrgQueryHandler : IBaseAsyncQueryHandler<UserSearchByRequestOrgQuery, List<RequestTargetMobileView>>
    {
        private readonly IRequestTargetQueryService _queryService;

        public UserRequestTargetSearchByRequestOrgQueryHandler(IRequestTargetQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<List<RequestTargetMobileView>> HandleAsync(UserSearchByRequestOrgQuery query)
        {
            var res = await _queryService.GetByPredicate(_ => true);
            return res.Select(m => m.GetRequestTargetMobileView()).ToList();
        }
    }
}