using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;

namespace MozaeekCore.Facade.Query
{
    public class UnProcessedRequestQueryFacade : IUnProcessedRequestQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public UnProcessedRequestQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }
        public Task<UnProcessedRequestDto> GetProcessedRequestById(int id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, UnProcessedRequestDto>(new FindByKey(id));
        }
    }
}