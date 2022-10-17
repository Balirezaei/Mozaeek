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
    public class UserRequestTargetSearchBySubjectQueryHandler : IBaseAsyncQueryHandler<UserDashboardSearchBySubjectQuery, List<RequestTargetMobileView>>
    {
        private readonly IRequestTargetQueryService _queryService;

        public UserRequestTargetSearchBySubjectQueryHandler(IRequestTargetQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<List<RequestTargetMobileView>> HandleAsync(UserDashboardSearchBySubjectQuery query)
        {
            var res = await _queryService.GetAllRequestTargetBySubjects(query);
            return res.Select(m => m.GetRequestTargetMobileView()).ToList();
        }
    }
}