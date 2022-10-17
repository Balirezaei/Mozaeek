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
    public class GetInitSubjectPriceQueryHandler : IBaseAsyncQueryHandler<Nothing, InitSubjectPriceDto>
    {
        private readonly IGenericRepository<PriceCurrency> _priceCurrencyIdRepository;
        private readonly ISubjectQueryService _subjectQueryService;

        public GetInitSubjectPriceQueryHandler(IGenericRepository<PriceCurrency> priceCurrencyIdRepository, ISubjectQueryService subjectQueryService)
        {
            _priceCurrencyIdRepository = priceCurrencyIdRepository;
            _subjectQueryService = subjectQueryService;
        }

        public async Task<InitSubjectPriceDto> HandleAsync(Nothing query)
        {
            var units = await _priceCurrencyIdRepository.GetAll()
                .Select(m => new UnitPriceDto() { Id = m.Id, Title = m.Unit })
                .ToListAsync();

            var subjects = await _subjectQueryService.GetByPredicate(m => m.HasChild == false);

            var res = new InitSubjectPriceDto()
            {
                UnitPrices = units,
                SubjectList = subjects.Select(m => m.GetSubjectDto()).ToList()
            };
            return res;
        }
    }
}