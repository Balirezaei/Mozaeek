using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllLabelChildrenQueryHandler : IBaseAsyncQueryHandler<FindByKey, List<LabelGrid>>
    {
        private readonly ILabelQueryService _labelQueryService;

        public GetAllLabelChildrenQueryHandler(ILabelQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<List<LabelGrid>> HandleAsync(FindByKey query)
        {
            var querys = await _labelQueryService.GetByPredicate(m => m.ParentId == query.Id);
            return querys.Select(m => new LabelGrid
            {
                Id = m.Id,
                Title = m.Title,
                HasChild = m.HasChild
            }).ToList();
        }
    }
}