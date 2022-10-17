using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper.Announcement;
using MozaeekCore.Persistense.EF.Repository;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetNewsForProcessByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, NewsForProcess>
    {
        private readonly INewsToProcessRepository _newsToProcessRepository;

        public GetNewsForProcessByIdQueryHandler(INewsToProcessRepository newsToProcessRepository)
        {
            _newsToProcessRepository = newsToProcessRepository;
        }

        public async Task<NewsForProcess> HandleAsync(FindByKey query)
        {
            var news = await _newsToProcessRepository.GetRssNewsById(query.Id);
            return news.GetNewForProcess();
        }
    }
}