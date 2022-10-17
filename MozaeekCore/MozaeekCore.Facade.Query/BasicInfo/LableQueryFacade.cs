using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Enum;

namespace MozaeekCore.Facade.Query
{
    public class LabelQueryFacade : ILabelQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public LabelQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<LabelDto> GetLabelById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, LabelDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<LabelGrid>> GetAllLabelChildren(long parentId)
        {
            var list = await _queryProcessor.ProcessAsync<FindByKey, List<LabelGrid>>(new FindByKey(parentId));
            return new PagedListResult<LabelGrid>()
            {
                List = list,
                TotalCount = list.Count,
                PageNumber = 1,
                PageSize = 1
            };
        }

        public async Task<PagedListResult<LabelGrid>> GetAllParentLabels(LabelFilterContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<LabelFilterContract, List<LabelGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, LabelTotalCount>(new Nothing());
            return new PagedListResult<LabelGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

        public Task<InitLabelDto> GetInitLabelDto()
        {
            return _queryProcessor.ProcessAsync<Nothing, InitLabelDto>(new Nothing());
        }

        public Task<List<SynonymsDto>> GetAllSynonym()
        {
            return _queryProcessor.ProcessAsync<FindSynonymByEntityType, List<SynonymsDto>>(new FindSynonymByEntityType(EntityType.Label));
        }

        public async Task<PagedListResult<LabelGrid>> GetAllLevelLabelChildrenByTextSearch(string query)
        {
            var list= await _queryProcessor.ProcessAsync<FindByTextSearch, List<LabelGrid>>(new FindByTextSearch(query));
            return new PagedListResult<LabelGrid>()
            {
                List = list,
                TotalCount = list.Count,
                PageNumber = 1,
                PageSize = 1
            };
        }
    }

    public interface ILabelQueryFacade
    {
        Task<LabelDto> GetLabelById(long id);
        Task<PagedListResult<LabelGrid>> GetAllParentLabels(LabelFilterContract pagingContract);
        Task<PagedListResult<LabelGrid>> GetAllLabelChildren(long parentId);
        Task<InitLabelDto> GetInitLabelDto();
        Task<List<SynonymsDto>> GetAllSynonym();
        Task<PagedListResult<LabelGrid>> GetAllLevelLabelChildrenByTextSearch(string query);
    }

}