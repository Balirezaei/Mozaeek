using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetPointInitQueryHandler : IBaseAsyncQueryHandler<Nothing, InitPointDto>
    {
        private readonly IGenericRepository<Point> _repository;

        public GetPointInitQueryHandler(IGenericRepository<Point> repository)
        {
            _repository = repository;
        }
        public async Task<InitPointDto> HandleAsync(Nothing query)
        {
            var res = (await _repository.GetAll().Select(m => BasicInfoProfile.GetPointDtoNoParent(m)).ToListAsync());
            return new InitPointDto()
            {
                Points = res
            };
        }
    }
}