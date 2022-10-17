using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetLabelsCountQueryHandler : IBaseAsyncQueryHandler<Nothing, LabelTotalCount>
    {
        private readonly ILabelQueryService _labelQueryService;


        public GetLabelsCountQueryHandler(ILabelQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<LabelTotalCount> HandleAsync(Nothing query)
        {
            long count = await _labelQueryService.GetCount(m => m.ParentId == null);
            return new LabelTotalCount(count);
        }
    }
}