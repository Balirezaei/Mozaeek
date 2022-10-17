using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public class RequestPriceQueryFacade : IRequestPriceQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public RequestPriceQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<RequestPriceDto> GetRequestPriceById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, RequestPriceDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<RequestPriceGrid>> GetAllRequestPrices(PagingContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<RequestPriceGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, RequestPriceTotalCount>(new Nothing());
            return new PagedListResult<RequestPriceGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

        public Task<InitRequestPriceDto> GetInitRequestPriceDto()
        {
            return _queryProcessor.ProcessAsync<Nothing, InitRequestPriceDto>(new Nothing());
        }
    }

    public interface IRequestPriceQueryFacade
    {
        Task<RequestPriceDto> GetRequestPriceById(long id);
        Task<PagedListResult<RequestPriceGrid>> GetAllRequestPrices(PagingContract pagingContract);
        Task<InitRequestPriceDto> GetInitRequestPriceDto();
    }

}