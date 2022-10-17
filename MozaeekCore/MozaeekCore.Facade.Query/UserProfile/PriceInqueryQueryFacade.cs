using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;

namespace MozaeekCore.Facade.Query.UserProfile
{
    public interface IPriceInqueryQueryFacade
    {
        Task<ProperPriceRequestQuestion> GetProperPriceForRequest(long requestId);
        Task<ProperPriceSubjectQuestion> GetProperPriceForSubject(long subjectId);
    }

    public class PriceInqueryQueryFacade : IPriceInqueryQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public PriceInqueryQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<ProperPriceRequestQuestion> GetProperPriceForRequest(long requestId)
        {
            return _queryProcessor.ProcessAsync<ProperPriceForRequestQuestionQuery, ProperPriceRequestQuestion>(
                new ProperPriceForRequestQuestionQuery(requestId));
        }

        public Task<ProperPriceSubjectQuestion> GetProperPriceForSubject(long subjectId)
        {
            return _queryProcessor.ProcessAsync<ProperPriceForSubjectQuestionQuery, ProperPriceSubjectQuestion>(
                new ProperPriceForSubjectQuestionQuery(subjectId));
        }

    }
}