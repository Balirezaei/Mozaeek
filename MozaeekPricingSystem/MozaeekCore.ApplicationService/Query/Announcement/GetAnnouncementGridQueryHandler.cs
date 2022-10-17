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
    public class GetAnnouncementGridQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<AnnouncementGrid>>
    {
        private IAnnouncementQueryService _announcementQueryService;

        public GetAnnouncementGridQueryHandler(IAnnouncementQueryService announcementQueryService)
        {
            _announcementQueryService = announcementQueryService;
        }

        public async Task<List<AnnouncementGrid>> HandleAsync(PagingContract query)
        {
            var querys =
                await _announcementQueryService.GetByPredicate(m => m.Id > 0,
                    new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));
            
            return querys.Select(m => m.GetAnnouncementGrid()).ToList();
        }
    }
}