using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Pricing;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetInitRequestPriceQueryHandler : IBaseAsyncQueryHandler<Nothing, InitRequestPriceDto>
    {
        private readonly IGenericRepository<PriceCurrency> _priceCurrencyIdRepository;
        private readonly IRequestQueryService _requestQueryService;

        public GetInitRequestPriceQueryHandler(IGenericRepository<PriceCurrency> priceCurrencyIdRepository, IRequestQueryService requestQueryService)
        {
            _priceCurrencyIdRepository = priceCurrencyIdRepository;
            _requestQueryService = requestQueryService;
        }
        public async Task<InitRequestPriceDto> HandleAsync(Nothing query)
        {
            var units = await _priceCurrencyIdRepository.GetAll()
                .Select(m => new UnitPriceDto() { Id = m.Id, Title = m.Unit })
                .ToListAsync();
            var requests = await _requestQueryService.GetByPredicate(_ => true);
            var res = new InitRequestPriceDto()
            {
                UnitPrices = units,
                RequestList = requests.Select(m => m.GetRequestGrid()).ToList()
            };
            return res;
        }
    }
}