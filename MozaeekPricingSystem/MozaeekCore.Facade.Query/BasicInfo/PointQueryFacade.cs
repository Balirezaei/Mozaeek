using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;

namespace MozaeekCore.Facade.Query
{
    public interface IPointQueryFacade
    {
        Task<PointDto> GetPointById(long id);
        Task<InitPointDto> GetInitPointDto();
        Task<PagedListResult<PointGrid>> GetAllParentPoints(PointFilterContract pagingContract);
        Task<PagedListResult<PointGrid>> GetAllPointChildren(long id);
    }
    public class PointQueryFacade : IPointQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public PointQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public Task<PointDto> GetPointById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, PointDto>(new FindByKey(id));
        }
        
        public Task<InitPointDto> GetInitPointDto()
        {
            return _queryProcessor.ProcessAsync<Nothing, InitPointDto>(new Nothing());
        }

        public async Task<PagedListResult<PointGrid>> GetAllParentPoints(PointFilterContract pagingContract)
        {
            var list = await _queryProcessor.ProcessAsync<PointFilterContract, List<PointGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<Nothing, PointTotalCount>(new Nothing());
            return new PagedListResult<PointGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }

        public async Task<PagedListResult<PointGrid>> GetAllPointChildren(long id)
        {
            var list = await _queryProcessor.ProcessAsync<FindByKey, List<PointGrid>>(new FindByKey(id));
            return new PagedListResult<PointGrid>()
            {
                List = list,
                TotalCount = list.Count,
                PageNumber = 1,
                PageSize = 1
            };
        }
    }
    
}