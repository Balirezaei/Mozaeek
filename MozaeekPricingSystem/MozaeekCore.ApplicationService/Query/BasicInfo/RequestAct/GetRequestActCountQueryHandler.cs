using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetRequestActCountQueryHandler : IBaseAsyncQueryHandler<Nothing, RequestActTotalCount>
    {
        private readonly IGenericRepository<RequestAct> _repository;

        public GetRequestActCountQueryHandler(IGenericRepository<RequestAct> repository)
        {
            _repository = repository;
        }

        public async Task<RequestActTotalCount> HandleAsync(Nothing query)
        {
            long count = await _repository.GetByPredicate(m => m.Title != "").CountAsync();
            return new RequestActTotalCount(count);
        }
    }
}