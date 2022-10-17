using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper.Announcement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllAnnouncementRequestGridQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<AnnouncementRequestGrid>>
    {
        private readonly IAnnouncementQueryService _announcementQueryService;

        public GetAllAnnouncementRequestGridQueryHandler(IAnnouncementQueryService announcementQueryService)
        {
            _announcementQueryService = announcementQueryService;
        }

        public async Task<List<AnnouncementRequestGrid>> HandleAsync(PagingContract query)
        {
            var news = await _announcementQueryService.GetByPredicate(m => m.HasRequest == true, query);
            return news.Select(m => m.GetAnnouncementRequestGrid()).ToList();
        }
    }
}