using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Query.BasicInfo.Subject
{
    public class GetAllSubjectByTextQueryHandler : IBaseAsyncQueryHandler<FindByTextSearch, List<SubjectGrid>>
    {
        private readonly ISubjectQueryService _subjectQueryService;

        public GetAllSubjectByTextQueryHandler(ISubjectQueryService subjectQueryService)
        {
            _subjectQueryService = subjectQueryService;
        }

        public async Task<List<SubjectGrid>> HandleAsync(FindByTextSearch query)
        {
            var querys = await _subjectQueryService.GetAllByText(query.Query);
            return querys.Select(m => m.GetSubjectGrid()).ToList();
        }
    }
}
