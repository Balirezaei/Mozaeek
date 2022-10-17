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
    public class UserRequestTargetSearchByUserCharacteristicQueryHandler : IBaseAsyncQueryHandler<UserDashboardSearchByCharactresticQuery, List<RequestTargetMobileView>>
    {
        private readonly IRequestTargetQueryService _queryService;

        public UserRequestTargetSearchByUserCharacteristicQueryHandler(IRequestTargetQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<List<RequestTargetMobileView>> HandleAsync(UserDashboardSearchByCharactresticQuery query)
        {
            var res = await _queryService.GetAllRequestTargetByCharacteristic(query);
            return res.Select(m => RequestTargetProfile.GetRequestTargetMobileView(m)).ToList();
        }
    }
}