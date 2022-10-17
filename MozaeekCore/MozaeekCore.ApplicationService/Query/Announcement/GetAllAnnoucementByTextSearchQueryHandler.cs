using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Mapper.Announcement;

namespace MozaeekCore.ApplicationService.Query.Announcement
{

    public class GetAllAnnoucementByTextSearchQueryHandler : IBaseAsyncQueryHandler<FindByTextSearch, List<UserAnnouncementDto>>
    {
        private readonly IAnnouncementQueryService _queryService;
        public GetAllAnnoucementByTextSearchQueryHandler(IAnnouncementQueryService queryService, ILabelQueryService labelQueryService)
        {
            _queryService = queryService;
      
        }
        public async Task<List<UserAnnouncementDto>> HandleAsync(FindByTextSearch query)
        {
            var querys = await _queryService.GetAllAnnouncementByTextSearch(query.Query);
            return querys.Select(q => q.GetUserAnnouncementDto()).ToList();
        }
    }
}
