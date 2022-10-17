using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper.Announcement;
using MozaeekCore.Persistense.EF.Repository;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllNewsReadyToProcessQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<NewsForProcessGrid>>
    {
        private readonly INewsToProcessRepository _newsToProcessRepository;

        public GetAllNewsReadyToProcessQueryHandler(INewsToProcessRepository newsToProcessRepository)
        {
            _newsToProcessRepository = newsToProcessRepository;
        }

        public async Task<List<NewsForProcessGrid>> HandleAsync(PagingContract query)
        {
            var news = await _newsToProcessRepository.GetAllNewsReadyToProcess(query);
            return news.Select(m => m.GetNewForProcessGrid()).ToList();

        }
    }
}