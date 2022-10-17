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
    //public class UserRequestTargetSearchByLabelQueryHandler : IBaseAsyncQueryHandler<UserSearchByLabelQuery, List<RequestTargetMobileView>>
    //{
    //    private readonly IRequestTargetQueryService _queryService;

    //    public UserRequestTargetSearchByLabelQueryHandler(IRequestTargetQueryService queryService)
    //    {
    //        _queryService = queryService;
    //    }

    //    public async Task<List<RequestTargetMobileView>> HandleAsync(UserSearchByLabelQuery query)
    //    {
    //        var res = await _queryService.GetByPredicate(_ => true);
    //        return res.Select(m => m.GetRequestTargetMobileView()).ToList();
    //    }
    //}
}