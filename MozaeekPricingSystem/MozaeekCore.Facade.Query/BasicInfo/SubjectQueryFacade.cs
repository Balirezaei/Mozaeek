using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;

namespace MozaeekCore.Facade.Query
{
    public class SubjectQueryFacade : ISubjectQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public SubjectQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<SubjectDto> GetSubjectById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, SubjectDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<SubjectGrid>> GetAllSubjectChildren(long parentId)
        {
            var list = await _queryProcessor.ProcessAsync<FindByKey, List<SubjectGrid>>(new FindByKey(parentId));
            return new PagedListResult<SubjectGrid>()
            {
                List = list,
                TotalCount = list.Count,
                PageNumber = 1,
                PageSize = 1
            };
        }

        public async Task<PagedListResult<SubjectGrid>> GetAllParentSubjects(SubjectFilterContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<SubjectFilterContract, List<SubjectGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, SubjectTotalCount>(new Nothing());
            return new PagedListResult<SubjectGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

        public Task<InitSubjectDto> GetInitSubjectDto()
        {
            return _queryProcessor.ProcessAsync<Nothing, InitSubjectDto>(new Nothing());
        }
    }

    public interface ISubjectQueryFacade
    {
        Task<SubjectDto> GetSubjectById(long id);
        Task<PagedListResult<SubjectGrid>> GetAllParentSubjects(SubjectFilterContract pagingContract);
        Task<PagedListResult<SubjectGrid>> GetAllSubjectChildren(long parentId);
        Task<InitSubjectDto> GetInitSubjectDto();
    }

}