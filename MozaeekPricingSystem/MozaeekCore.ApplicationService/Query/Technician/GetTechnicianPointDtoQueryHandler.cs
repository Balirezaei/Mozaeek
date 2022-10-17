using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetTechnicianPointDtoQueryHandler : IBaseAsyncQueryHandler<FindByKey, TechnicianPointDto>
    {
        private readonly ITechnicianRepository repository;

        public GetTechnicianPointDtoQueryHandler(ITechnicianRepository repository)
        {
            this.repository = repository;
        }
        public async Task<TechnicianPointDto> HandleAsync(FindByKey query)
        {
            var res = await repository.FindWithPoint(query.Id);
            var info = new TechnicianPointDto()
            {
                TechnicianId = res.Id
            };

            if (res.TechnicianPoints != null)
            {
                info.Points = res.TechnicianPoints.Select(m => m.PointId).ToList();
            }

            return info;
        }
    }
}