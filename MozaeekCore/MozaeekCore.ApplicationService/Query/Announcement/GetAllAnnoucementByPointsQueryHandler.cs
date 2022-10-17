using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper.Announcement;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.Announcement
{

    public class GetAllAnnoucementByPointsQueryHandler : IBaseAsyncQueryHandler<FindByListKey, List<UserAnnouncementDto>>
    {
        private readonly IAnnouncementQueryService _queryService;
        public GetAllAnnoucementByPointsQueryHandler(IAnnouncementQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<List<UserAnnouncementDto>> HandleAsync(FindByListKey input)
        {
            var querys = await _queryService.GetAllAnnouncementByPoints(input.Ids);
            return querys.Select(q => q.GetUserAnnouncementDto()).ToList();

        }
    }
}
