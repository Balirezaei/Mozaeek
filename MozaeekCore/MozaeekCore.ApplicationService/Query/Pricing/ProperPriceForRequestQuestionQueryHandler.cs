using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb.Pricing;

namespace MozaeekCore.ApplicationService.Query
{
    public class ProperPriceForRequestQuestionQueryHandler : IBaseAsyncQueryHandler<ProperPriceForRequestQuestionQuery, ProperPriceRequestQuestion>
    {
        private readonly IRequestPriceQueryService _requestPriceQueryService;

        public ProperPriceForRequestQuestionQueryHandler(IRequestPriceQueryService requestPriceQueryService)
        {
            _requestPriceQueryService = requestPriceQueryService;
        }

        public async Task<ProperPriceRequestQuestion> HandleAsync(ProperPriceForRequestQuestionQuery query)
        {
            var requestPrice = await _requestPriceQueryService.GetProperRequestPriceByRequestId(query.RequestId);
            if (requestPrice == null)
            {
                return null;
            }

            var request = requestPrice.RequestPriceDetails.FirstOrDefault(m => m.RequestId == query.RequestId);

            return requestPrice.GetProperRequestPriceResult(request.RequestTitle,request.FullOnline);
        }
    }
}