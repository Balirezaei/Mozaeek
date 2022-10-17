using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.Lable
{
    public class GetAllLabelByTextSearchQueryHandler : IBaseAsyncQueryHandler<FindByTextSearch, List<LabelGrid>>
    {
        private readonly ILabelQueryService _labelQueryService;

        public GetAllLabelByTextSearchQueryHandler(ILabelQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<List<LabelGrid>> HandleAsync(FindByTextSearch query)
        {
            var querys = await _labelQueryService.GetAllByTextSearch(query.Query);
            return querys.Select(m => m.GetLabelGrid()).ToList();
        }
    }


}
