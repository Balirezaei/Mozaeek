using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.BasicInfo.RequestTarget;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query.RequestRoot.RequestTarget
{
    public class GetAllRequestTargetByTextSearchQueryHandler : IBaseAsyncQueryHandler<FindByTextSearch, List<RequestTargetGrid>>
    {
        private readonly IRequestTargetQueryService _requestTargetQueryService;

        public GetAllRequestTargetByTextSearchQueryHandler(IRequestTargetQueryService requestTargetQueryService)
        {
            _requestTargetQueryService = requestTargetQueryService;
        }

        public async Task<List<RequestTargetGrid>> HandleAsync(FindByTextSearch query)
        {
            var querys =await _requestTargetQueryService.GetAllRequestTargetByText(query.Query);
            return querys.Select(q => q.GetRequestTargetGrid()).ToList();
        }
    }
}
