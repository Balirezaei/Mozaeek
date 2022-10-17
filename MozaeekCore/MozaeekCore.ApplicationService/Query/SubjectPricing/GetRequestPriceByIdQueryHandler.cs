using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.Persistense.MongoDb.Pricing;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetSubjectPriceByIdQueryHandler : IBaseAsyncQueryHandler<FindByKey, SubjectPriceDto>
    {
        private readonly ISubjectPriceQueryService _subjectPriceQueryService;
        private readonly ISubjectQueryService _subjectQueryService;
        public GetSubjectPriceByIdQueryHandler( ISubjectPriceQueryService subjectPriceQueryService)
        {
            _subjectPriceQueryService = subjectPriceQueryService;
        }
        public async Task<SubjectPriceDto> HandleAsync(FindByKey query)
        {
            var res = await _subjectPriceQueryService.Get(query.Id);

            return res.GetSubjectPriceDto();
        }
    }
}