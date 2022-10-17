using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.ApplicationService.Contract.UserProfile;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper.Announcement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query.Announcement
{
    public class GetAllAnnoucementByUserDashboardSearchByCharactresticQueryQueryHandler : IBaseAsyncQueryHandler<UserDashboardSearchByCharactresticQuery, List<UserAnnouncementDto>>
    {
        private readonly IAnnouncementQueryService _queryService;
        public GetAllAnnoucementByUserDashboardSearchByCharactresticQueryQueryHandler(IAnnouncementQueryService queryService, ILabelQueryService labelQueryService)
        {
            _queryService = queryService;

        }
        public async Task<List<UserAnnouncementDto>> HandleAsync(UserDashboardSearchByCharactresticQuery query)
        {
            var querys = await _queryService.GetAllAnnouncementByLabels(query.LabelIds);
            return querys.Select(q => AnnouncementProfile.GetUserAnnouncementDto(q)).ToList();
        }
    }
}