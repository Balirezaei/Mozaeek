using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Mapper.Announcement;

namespace MozaeekCore.ApplicationService.Query.Announcement
{
    public class GetAllAnnouncmentUserDashboardQuery : IBaseAsyncQueryHandler<AnnouncementUserDashboardPagingContract, List<UserAnnouncementDto>>
    {
        private readonly IAnnouncementQueryService _queryService;
        public GetAllAnnouncmentUserDashboardQuery(IAnnouncementQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<List<UserAnnouncementDto>> HandleAsync(AnnouncementUserDashboardPagingContract query)
        {
            var querys = await _queryService.GetAllAnnouncementForUserDashboard(query);
           return querys.Select(q => q.GetUserAnnouncementDto()).ToList();
            
        }
    }
}
