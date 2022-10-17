using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public class SubjectPriceQueryFacade : ISubjectPriceQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public SubjectPriceQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<SubjectPriceDto> GetSubjectPriceById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, SubjectPriceDto>(new FindByKey(id));
        }
        
        public async Task<PagedListResult<SubjectPriceGrid>> GetAllSubjectPrices(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<SubjectPriceGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, SubjectPriceTotalCount>(new Nothing());
            return new PagedListResult<SubjectPriceGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

        public Task<InitSubjectPriceDto> GetInitSubjectPriceDto()
        {
            return _queryProcessor.ProcessAsync<Nothing, InitSubjectPriceDto>(new Nothing());
        }
    }

    public interface ISubjectPriceQueryFacade
    {
        Task<SubjectPriceDto> GetSubjectPriceById(long id);
        Task<PagedListResult<SubjectPriceGrid>> GetAllSubjectPrices(PagingContract pagingContract);
        Task<InitSubjectPriceDto> GetInitSubjectPriceDto();
    }

}