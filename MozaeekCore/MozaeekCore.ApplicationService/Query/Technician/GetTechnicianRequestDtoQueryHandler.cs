using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Query
{
    //public class GetTechnicianRequestDtoQueryHandler : IBaseAsyncQueryHandler<FindByKey, TechnicianRequestDto>
    //{
    //    private readonly ITechnicianRepository repository;

    //    public GetTechnicianRequestDtoQueryHandler(ITechnicianRepository repository)
    //    {
    //        this.repository = repository;
    //    }
    //    public async Task<TechnicianRequestDto> HandleAsync(FindByKey query)
    //    {
    //        var res = await repository.FindWithRequest(query.Id);
    //        var info = new TechnicianRequestDto()
    //        {
    //            TechnicianId = res.Id
    //        };

    //        if (res.TechnicianRequests != null)
    //        {
    //            info.Requests = res.TechnicianRequests.Select(m => m.RequestId).ToList();
    //        }

    //        return info;
    //    }
    //}
}