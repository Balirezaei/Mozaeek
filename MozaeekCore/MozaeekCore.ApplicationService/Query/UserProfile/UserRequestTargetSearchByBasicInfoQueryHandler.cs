using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.UserProfile;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper.Announcement;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.UserProfile
{
    public class UserRequestTargetSearchByBasicInfoQueryHandler : IBaseAsyncQueryHandler<UserSearchByBasicInfoQuery, List<RequestTargetMobileView>>
    {
        private readonly IRequestTargetQueryService _queryService;

        public UserRequestTargetSearchByBasicInfoQueryHandler(IRequestTargetQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<List<RequestTargetMobileView>> HandleAsync(UserSearchByBasicInfoQuery query)
        {
            var res = await _queryService.GetAllRequestTargetByBasicInfo(query);
            return res.Select(m => m.GetRequestTargetMobile()).ToList();
        }
    }
}
