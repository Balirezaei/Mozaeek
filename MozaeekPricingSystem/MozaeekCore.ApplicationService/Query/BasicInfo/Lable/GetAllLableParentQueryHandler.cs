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
    public class GetAllLabelParentQueryHandler : IBaseAsyncQueryHandler<LabelFilterContract, List<LabelGrid>>
    {
        private readonly ILabelQueryService _labelQueryService;

        public GetAllLabelParentQueryHandler(ILabelQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<List<LabelGrid>> HandleAsync(LabelFilterContract query)
        {
            var querys =
                await _labelQueryService.GetByPredicate(m => m.ParentId == null, new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));
            
            return querys.Select(m => new LabelGrid
            {
                Id = m.Id,
                Title = m.Title,
                HasChild = m.HasChild
            }).ToList();
        }
    }
}