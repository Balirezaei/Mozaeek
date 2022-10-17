using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllPreRequestQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<PreRequestGrid>>
    {
        private readonly IPreRequestQueryService _preRequestQueryService;

        public GetAllPreRequestQueryHandler(IPreRequestQueryService preRequestQueryService)
        {
            _preRequestQueryService = preRequestQueryService;
        }

        public async Task<List<PreRequestGrid>> HandleAsync(PagingContract query)
        {
            var querys =
                await _preRequestQueryService.GetByPredicate(_ => true, new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));

            return querys.Select(m => new PreRequestGrid
            {
                Id = m.Id,
                Title = m.Title,
                IsProcessed = m.IsProcessed
            }).ToList();
        }
    }
}