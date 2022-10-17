using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.ApplicationService.Contract.UserProfile;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper.Announcement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query.UserProfile
{
    //ToDo : Check This
    //public class UserAnnouncementSearchByRequestTargetQueryHandler : IBaseAsyncQueryHandler<UserSearchByRequestTargetQuery, List<UserAnnouncementDto>>
    //{
    //    private readonly IAnnouncementQueryService _queryService;

    //    public UserAnnouncementSearchByRequestTargetQueryHandler(IAnnouncementQueryService queryService)
    //    {
    //        _queryService = queryService;
    //    }

    //    public async Task<List<UserAnnouncementDto>> HandleAsync(UserSearchByRequestTargetQuery query)
    //    {
    //        var res = await _queryService.GetByPredicate(m => m.RequestTarget.Id == query.RequestTargetId);
    //        return res.Select(m => m.GetUserAnnouncementDto()).ToList();
    //    }
    //}
}