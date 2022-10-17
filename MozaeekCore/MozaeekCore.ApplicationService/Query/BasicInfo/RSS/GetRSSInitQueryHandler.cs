using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRSSInitQueryHandler : IBaseAsyncQueryHandler<Nothing, InitRSSDto>
    {
        private readonly IGenericRepository<RSS> _repository;

        public GetRSSInitQueryHandler(IGenericRepository<RSS> repository)
        {
            _repository = repository;
        }
        public async Task<InitRSSDto> HandleAsync(Nothing query)
        {
            var res = (await _repository.GetAll().Select(m => m.GetRSSDto()).ToListAsync());
            return new InitRSSDto()
            {
                RSSs = res
            };
        }
    }
}