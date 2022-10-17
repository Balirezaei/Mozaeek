using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    //public class GetTechnicianTotalCountQueryHandler : IBaseAsyncQueryHandler<PagingContract, TechnicianTotalCount>
    //{
    //    private readonly ITechnicianQueryService _queryService;

    //    public GetTechnicianTotalCountQueryHandler(ITechnicianQueryService queryService)
    //    {
    //        _queryService = queryService;
    //    }

    //    public async Task<TechnicianTotalCount> HandleAsync(PagingContract query)
    //    {
    //        var count = await _queryService.GetCount(_ => true);

    //        return new TechnicianTotalCount(count);
    //    }
    //}
}