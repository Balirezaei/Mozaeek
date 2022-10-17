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
    //public class UserAnnouncementSearchByLabelQueryHandler : IBaseAsyncQueryHandler<UserSearchByLabelQuery, List<UserAnnouncementDto>>
    //{
    //    private readonly IAnnouncementQueryService _queryService;

    //    public UserAnnouncementSearchByLabelQueryHandler(IAnnouncementQueryService queryService)
    //    {
    //        _queryService = queryService;
    //    }

    //    public async Task<List<UserAnnouncementDto>> HandleAsync(UserSearchByLabelQuery query)
    //    {
    //        var res = await _queryService.GetByPredicate(_ => true);
    //        return res.Select(m => m.GetUserAnnouncementDto()).ToList();
    //    }
    //}
}