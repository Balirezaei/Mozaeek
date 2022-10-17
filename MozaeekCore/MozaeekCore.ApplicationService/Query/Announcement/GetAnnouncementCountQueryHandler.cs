using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAnnouncementCountQueryHandler : IBaseAsyncQueryHandler<Nothing, AnnouncementTotalCount>
    {
        private readonly IAnnouncementQueryService _queryService;

        public GetAnnouncementCountQueryHandler(IAnnouncementQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<AnnouncementTotalCount> HandleAsync(Nothing query)
        {
            long count = await _queryService.GetCount(_ => true);
            return new AnnouncementTotalCount(count);
        }
    }
}