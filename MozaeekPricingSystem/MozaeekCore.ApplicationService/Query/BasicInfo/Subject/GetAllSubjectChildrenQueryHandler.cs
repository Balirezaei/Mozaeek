using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetAllSubjectChildrenQueryHandler : IBaseAsyncQueryHandler<FindByKey, List<SubjectGrid>>
    {
        private readonly ISubjectQueryService _labelQueryService;

        public GetAllSubjectChildrenQueryHandler(ISubjectQueryService labelQueryService)
        {
            _labelQueryService = labelQueryService;
        }

        public async Task<List<SubjectGrid>> HandleAsync(FindByKey query)
        {
            var querys = await _labelQueryService.GetByPredicate(m => m.ParentId == query.Id);
            return querys.Select(m => new SubjectGrid
            {
                Id = m.Id,
                Title = m.Title,
                HasChild = m.HasChild
            }).ToList();
        }
    }
}