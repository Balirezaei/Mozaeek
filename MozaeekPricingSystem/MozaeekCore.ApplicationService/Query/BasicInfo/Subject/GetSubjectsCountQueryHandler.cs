using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Query
{
    public class GetSubjectsCountQueryHandler : IBaseAsyncQueryHandler<Nothing, SubjectTotalCount>
    {
        private readonly ISubjectQueryService _subjectQueryService;


        public GetSubjectsCountQueryHandler(ISubjectQueryService subjectQueryService)
        {
            _subjectQueryService = subjectQueryService;
        }

        public async Task<SubjectTotalCount> HandleAsync(Nothing query)
        {
            long count = await _subjectQueryService.GetCount(m => m.ParentId == null);
            return new SubjectTotalCount(count);
        }
    }
}