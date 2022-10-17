using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Facade.Contract;

namespace MozaeekCore.Facade.Query
{
    public interface IPointQueryFacade
    {
        Task<PointDto> GetPointById(long id);
        Task<InitPointDto> GetInitPointDto();
        Task<PagedListResult<PointGrid>> GetAllParentPoints(PointFilterDto filterDto);
        Task<PagedListResult<PointGrid>> GetAllPointChildren(long id);
        Task<PagedListResult<PointGrid>> GetAllCity(PointFilterDto filterDto);
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

        public async Task<PagedListResult<PointGrid>> GetAllParentPoints(PointFilterDto filterDto)
        {
            var pagingContract = new PointFilterContract
            {
                PageNumber = filterDto.PageNumber,
                Order = filterDto.Order,
                Sort = filterDto.Sort,
                PageSize = filterDto.PageSize,
                ShowParent = true,
                Title = filterDto.Title
            };
            var list = await _queryProcessor.ProcessAsync<PointFilterContract, List<PointGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<PointFilterContract, PointTotalCount>(pagingContract);
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

        public async Task<PagedListResult<PointGrid>> GetAllCity(PointFilterDto filterDto)
        {
            var pagingContract = new PointFilterContract
            {
                PageNumber = filterDto.PageNumber,
                Order = filterDto.Order,
                Sort = filterDto.Sort,
                PageSize = filterDto.PageSize,
                ShowCity = true,
                Title = filterDto.Title
            };
            var list = await _queryProcessor.ProcessAsync<PointFilterContract, List<PointGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<PointFilterContract, PointTotalCount>(pagingContract);
            return new PagedListResult<PointGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = pagingContract.PageNumber,
                PageSize = pagingContract.PageSize,
            };
        }
    }
    
}