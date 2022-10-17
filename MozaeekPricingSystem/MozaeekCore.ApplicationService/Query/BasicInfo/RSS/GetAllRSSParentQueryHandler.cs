using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllRSSParentQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<RSSDto>>
    {
        private readonly IGenericRepository<RSS> _repository;

        public GetAllRSSParentQueryHandler(IGenericRepository<RSS> repository)
        {
            _repository = repository;
        }

        public Task<List<RSSDto>> HandleAsync(PagingContract query)
        {
            return _repository.GetAll()
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(m => m.GetRSSDto()).ToListAsync();
        }
    }
}