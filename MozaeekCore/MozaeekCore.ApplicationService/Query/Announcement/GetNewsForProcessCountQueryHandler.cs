using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.EF.Repository;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetNewsForProcessCountQueryHandler : IBaseAsyncQueryHandler<Nothing, NewsForProcessTotalCount>
    {
        private readonly INewsToProcessRepository _newsToProcessRepository;

        public GetNewsForProcessCountQueryHandler(INewsToProcessRepository newsToProcessRepository)
        {
            _newsToProcessRepository = newsToProcessRepository;
        }

        public async Task<NewsForProcessTotalCount> HandleAsync(Nothing query)
        {
            long count = await _newsToProcessRepository.GetTotalCountForProcess();
            return new NewsForProcessTotalCount(count);
        }
    }

}