using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRSSsCountQueryHandler : IBaseAsyncQueryHandler<Nothing, RSSTotalCount>
    {
        private readonly IGenericRepository<RSS> _repository;

        public GetRSSsCountQueryHandler(IGenericRepository<RSS> repository)
        {
            _repository = repository;
        }

        public async Task<RSSTotalCount> HandleAsync(Nothing query)
        {
            long count = await _repository.GetAll().CountAsync();
            return new RSSTotalCount(count);
        }
    }
}