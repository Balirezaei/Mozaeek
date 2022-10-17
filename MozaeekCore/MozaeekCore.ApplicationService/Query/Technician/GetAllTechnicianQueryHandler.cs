using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.Technician
{

    public class GetAllTechnicianQueryHandler : IBaseAsyncQueryHandler<Nothing, List<TechnicianDto>>
    {
        private readonly ITechnicianQueryService _queryService;
        public GetAllTechnicianQueryHandler(ITechnicianQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<List<TechnicianDto>> HandleAsync(Nothing input)
        {
            var querys = await _queryService.Get();
            return querys.Select(q => q.GetTechnicianDto()).ToList();

        }
    }
}
