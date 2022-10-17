using System.Linq;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb.Pricing;

namespace MozaeekCore.ApplicationService.Query
{
    public class ProperPriceForSubjectQuestionQueryHandler : IBaseAsyncQueryHandler<ProperPriceForSubjectQuestionQuery, ProperPriceSubjectQuestion>
    {
        private readonly ISubjectPriceQueryService _subjectPriceQueryService;

        public ProperPriceForSubjectQuestionQueryHandler(ISubjectPriceQueryService subjectPriceQueryService)
        {
            _subjectPriceQueryService = subjectPriceQueryService;
        }


        public async Task<ProperPriceSubjectQuestion> HandleAsync(ProperPriceForSubjectQuestionQuery query)
        {
            var requestPrice = await _subjectPriceQueryService.GetProperSubjectPriceBySubjectId(query.SubjectId);
            if (requestPrice==null)
            {
                return null;
            }
            var subject = requestPrice.SubjectPriceDetails.FirstOrDefault(m => m.SubjectId == query.SubjectId);
            return requestPrice.GetProperSubjectPriceResult(subject.SubjectTitle);
        }
    }
}