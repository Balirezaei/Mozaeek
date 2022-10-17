using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetPreRequestCountQueryHandler : IBaseAsyncQueryHandler<Nothing, PreRequestTotalCount>
    {
        private readonly ILabelQueryService _labelQueryService;

        public GetPreRequestCountQueryHandler(ILabelQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<PreRequestTotalCount> HandleAsync(Nothing query)
        {
            long count = await _labelQueryService.GetCount(m => m.ParentId == null);
            return new PreRequestTotalCount(count);
        }
    }
}