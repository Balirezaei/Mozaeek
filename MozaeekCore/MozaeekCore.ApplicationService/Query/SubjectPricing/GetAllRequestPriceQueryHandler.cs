using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb.Pricing;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllSubjectPriceQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<SubjectPriceGrid>>
    {
        private readonly ISubjectPriceQueryService _subjectTargetQueryService;

        public GetAllSubjectPriceQueryHandler(ISubjectPriceQueryService subjectTargetQueryService)
        {
            _subjectTargetQueryService = subjectTargetQueryService;
        }
        
        public async Task<List<SubjectPriceGrid>> HandleAsync(PagingContract query)
        {
            var querys =
                await _subjectTargetQueryService.GetByPredicate(_ => true,
                    new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));

            return querys.Select(m => m.GetSubjectPriceGrid()).ToList();
        }
    }
}