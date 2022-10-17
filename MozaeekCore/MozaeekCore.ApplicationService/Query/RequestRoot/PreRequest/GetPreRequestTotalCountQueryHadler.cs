using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetPreRequestTotalCountQueryHadler : IBaseAsyncQueryHandler<Nothing, PreRequestTotalCount>
    {
        private readonly IPreRequestQueryService _preRequestQueryService;

        public GetPreRequestTotalCountQueryHadler(IPreRequestQueryService preRequestQueryService)
        {
            _preRequestQueryService = preRequestQueryService;
        }


        public async Task<PreRequestTotalCount> HandleAsync(Nothing query)
        {
            long count = await _preRequestQueryService.GetCount(_ => true);
            return new PreRequestTotalCount(count);
        }
    }
}