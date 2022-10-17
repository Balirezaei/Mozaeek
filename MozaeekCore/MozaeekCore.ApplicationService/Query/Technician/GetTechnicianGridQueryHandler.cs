using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    //public class GetTechnicianGridQueryHandler : IBaseAsyncQueryHandler<PagingContract, List<TechnicianGridDto>>
    //{
    //    private readonly ITechnicianQueryService _queryService;

    //    public GetTechnicianGridQueryHandler(ITechnicianQueryService queryService)
    //    {
    //        _queryService = queryService;
    //    }

    //    public async Task<List<TechnicianGridDto>> HandleAsync(PagingContract query)
    //    {
    //        var list =
    //            await _queryService.GetByPredicate(_ => true,
    //                new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));
    //        return list.Select(m => m.GetTechnicianGrid()).ToList();
    //    }
    //}
}