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
    public class GetAllSubjectParentQueryHandler : IBaseAsyncQueryHandler<SubjectFilterContract, List<SubjectGrid>>
    {
        private readonly ISubjectQueryService _subjectQueryService;

        public GetAllSubjectParentQueryHandler(ISubjectQueryService subjectQueryService)
        {
            _subjectQueryService = subjectQueryService;
        }

        public async Task<List<SubjectGrid>> HandleAsync(SubjectFilterContract query)
        {
            var querys =
                await _subjectQueryService.GetByPredicate(m => m.ParentId == null, new PagingContract(query.PageSize, query.PageNumber, query.Sort, query.Order));
            
            return querys.Select(m => new SubjectGrid
            {
                Id = m.Id,
                Title = m.Title,
                HasChild = m.HasChild
            }).ToList();
        }
    }
}