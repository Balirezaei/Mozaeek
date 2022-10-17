using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllRequestActQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<RequestActDto>>
    {
        private readonly IRequestActQueryService _requestActQueryService;

        public GetAllRequestActQueryHandler(IRequestActQueryService requestActQueryService)
        {
            _requestActQueryService = requestActQueryService;
        }


        public async Task<List<RequestActDto>> HandleAsync(PagingContract query)
        {
            var querys =
                await _requestActQueryService.GetByPredicate(_ => true, new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));

            return querys.Select(m => new RequestActDto
            {
                Id = m.Id,
                Title = m.Title
            }).ToList();
        }
    }

}