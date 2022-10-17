using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb.Pricing;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetSubjectPriceCountQueryHandler : IBaseAsyncQueryHandler<Nothing, SubjectPriceTotalCount>
    {
        private readonly ISubjectPriceQueryService _subjectTargetQueryService;

        public GetSubjectPriceCountQueryHandler(ISubjectPriceQueryService subjectTargetQueryService)
        {
            _subjectTargetQueryService = subjectTargetQueryService;
        }

        public async Task<SubjectPriceTotalCount> HandleAsync(Nothing query)
        {
            long count = await _subjectTargetQueryService.GetCount(_ => true);
            return new SubjectPriceTotalCount(count);
        }

    }

}