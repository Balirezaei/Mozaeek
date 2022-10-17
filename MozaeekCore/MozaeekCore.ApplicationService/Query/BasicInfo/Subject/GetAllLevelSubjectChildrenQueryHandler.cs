using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace MozaeekCore.ApplicationService.Query.BasicInfo.Subject
{
    public class GetAllLevelSubjectChildrenQueryHandler : IBaseAsyncQueryHandler<FindByListKey, List<AllLevelSubjectChildren>>
    {
        private readonly ISubjectQueryService _subjectQueryService;

        public GetAllLevelSubjectChildrenQueryHandler(ISubjectQueryService subjectQueryService)
        {
            _subjectQueryService = subjectQueryService;
        }

        public async Task<List<AllLevelSubjectChildren>> HandleAsync(FindByListKey query)
        {
            var querys = await _subjectQueryService.GetAllLevelChildrenLookUp(query.Ids);
            return querys.Select(m => new AllLevelSubjectChildren
            {
                Id = m.Id,
            }).ToList();
        }
    }


}
