using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAnnouncementRequestTotalCountQueryHandler : IBaseAsyncQueryHandler<Nothing, AnnouncementRequestTotalCount>
    {
        private readonly IAnnouncementQueryService _announcementQueryService;

        public GetAnnouncementRequestTotalCountQueryHandler(IAnnouncementQueryService announcementQueryService)
        {
            _announcementQueryService = announcementQueryService;
        }

        public async Task<AnnouncementRequestTotalCount> HandleAsync(Nothing query)
        {
            var announcementCount = await _announcementQueryService.GetCount(m => m.HasRequest == true);
            return new AnnouncementRequestTotalCount(announcementCount);
        }
    }
}