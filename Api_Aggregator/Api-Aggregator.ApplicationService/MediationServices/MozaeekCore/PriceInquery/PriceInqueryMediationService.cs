using System.Net.Http;
using System.Threading.Tasks;
using Api_Aggregator.Infrastructure;
using Api_Aggregator.Infrastructure.ResponseMessages;
using Mozaeek.CR.PublicDto;

namespace Api_Aggregator.ApplicationService.MediationServices.MozaeekCore.PriceInquery
{
    public interface IPriceInqueryMediationService
    {
        Task<ProperPriceRequestQuestion> GetRequestQuestionPrice(long requestId);
        Task<ProperPriceSubjectQuestion> GetSubjectQuestionPrice(long subjectId);
    }


    public class PriceInqueryMediationService: IPriceInqueryMediationService
    {
        private readonly IHttpClientFactory _clientFactory;

        public PriceInqueryMediationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ProperPriceRequestQuestion> GetRequestQuestionPrice(long requestId)
        {
            var client = _clientFactory.CreateClient("MozaeekCore");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestGetAsync<Result<ProperPriceRequestQuestion>>("PriceInquery/GetProperPriceForRequest?requestId=" + requestId);
            return result.Data;
        }

        public async Task<ProperPriceSubjectQuestion> GetSubjectQuestionPrice(long subjectId)
        {
            var client = _clientFactory.CreateClient("MozaeekCore");
            var bsClient = new HttpClientRequestor(client);
            var result = await bsClient.RequestGetAsync<Result<ProperPriceSubjectQuestion>>("PriceInquery/GetProperPriceForSubject?subjectId=" + subjectId);
            return result.Data;
        }
    }
}