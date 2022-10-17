using System.Collections.Generic;
using System.Threading.Tasks;
using Mozaeek.CR.PublicDto;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Enum;

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
        public Task<List<SynonymsDto>> GetAllSynonym()
        {
            return _queryProcessor.ProcessAsync<FindSynonymByEntityType, List<SynonymsDto>>(new FindSynonymByEntityType(EntityType.Subject));
        }

        public async Task<SubjectWithPriceDetailDto> GetSubjectWithPriceDetail(long subjectId)
        {
            var properPrice = await _queryProcessor.ProcessAsync<ProperPriceForSubjectQuestionQuery, ProperPriceSubjectQuestion>(new ProperPriceForSubjectQuestionQuery(subjectId));
            if (properPrice == null)
            {
                var subject = await _queryProcessor.ProcessAsync<FindByKey, SubjectDto>(new FindByKey(subjectId));
                return new SubjectWithPriceDetailDto()
                {
                    SubjectTitle = subject.Title,
                    PriceAmount = 0,
                    TechnicianCount = 0,
                    SubjectId = subjectId
                };
            }
            else
            {
                return new SubjectWithPriceDetailDto()
                {
                    SubjectTitle = properPrice.SubjectTitle,
                    PriceAmount = properPrice.ProperPriceResult.UnitPrice,
                    TechnicianCount = 0,
                    SubjectId = subjectId
                };
            }
        }
    }

    public interface ISubjectQueryFacade
    {
        Task<SubjectDto> GetSubjectById(long id);
        Task<PagedListResult<SubjectGrid>> GetAllParentSubjects(SubjectFilterContract pagingContract);
        Task<PagedListResult<SubjectGrid>> GetAllSubjectChildren(long parentId);
        Task<InitSubjectDto> GetInitSubjectDto();
        Task<List<SynonymsDto>> GetAllSynonym();
        Task<SubjectWithPriceDetailDto> GetSubjectWithPriceDetail(long subjectId);
    }

}