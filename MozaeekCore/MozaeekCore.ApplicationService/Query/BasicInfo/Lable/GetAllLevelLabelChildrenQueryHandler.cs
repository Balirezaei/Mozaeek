using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.Lable
{
   public class GetAllLevelLabelChildrenQueryHandler : IBaseAsyncQueryHandler<FindByListKey, List<AllLevelLabelChildren>>
    {
        private readonly ILabelQueryService _labelQueryService;

        public GetAllLevelLabelChildrenQueryHandler(ILabelQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<List<AllLevelLabelChildren>> HandleAsync(FindByListKey query)
        {
            var querys = await _labelQueryService.GetAllLevelChildrenLookUp(query.Ids);
            return querys.Select(m => new AllLevelLabelChildren
            {
                Id = m.Id,
            }).ToList();
        }
    }
}
