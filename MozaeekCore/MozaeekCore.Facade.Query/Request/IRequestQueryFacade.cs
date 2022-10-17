using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.QueryHandler;
using MozaeekCore.Core.ResponseMessages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Facade.Contract;
using MozaeekCore.QueryModel;

namespace MozaeekCore.Facade.Query
{
    public interface IRequestQueryFacade
    {
        Task<RequestDto> GetRequestById(long id);
        Task<PagedListResult<RequestGrid>> GetAllRequests(RequestFilterDto filterDto);
        Task<InitRequestDto> GetInitRequestDto();
        Task<List<ConnectedRequestDto>> GetAllConnectedRequest(long id);
        Task<List<RequestGrid>> SearchRequest(RequestAutoCompleteDto dto);
        Task<PagedListResult<AnnouncementRequestGrid>> GetAllAnnouncementRequest(PagingContract contract);
        Task<List<RequestTargetAutocompleteResultDto>> SearchRequestTarget(RequestTargetAutoCompleteDto dto);
        Task<List<RequestGrid>> GetAllRequestsByRequestTarget(long requestTargetId);

    }

    public class RequestQueryFacade : IRequestQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public RequestQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }


        public Task<RequestDto> GetRequestById(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, RequestDto>(new FindByKey(id));
        }

        public async Task<PagedListResult<RequestGrid>> GetAllRequests(RequestFilterDto filterDto)
        {
            var pagingContract = new RequestPagingContract()
            {
                PageSize = filterDto.PageSize,
                Order = filterDto.Order,
                PageNumber = filterDto.PageNumber,
                Sort = filterDto.Sort,
                Title = filterDto.Title
            };
            var list = await _queryProcessor.ProcessAsync<RequestPagingContract, List<RequestGrid>>(pagingContract);
            var count = await _queryProcessor.ProcessAsync<RequestPagingContract, RequestTotalCount>(pagingContract);
            return new PagedListResult<RequestGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = filterDto.PageNumber,
                PageSize = filterDto.PageSize,
            };
        }

        public async Task<List<RequestGrid>> GetAllRequestsByRequestTarget(long requestTargetId)
        {
            var pagingContract = new RequestPagingContract()
            {
                PageSize = 100,
                PageNumber = 1,
                RequestTargetId = requestTargetId
            };
            return await _queryProcessor.ProcessAsync<RequestPagingContract, List<RequestGrid>>(pagingContract);
        }


        public async Task<InitRequestDto> GetInitRequestDto()
        {
            var res = await _queryProcessor.ProcessAsync<Nothing, InitRequestDto>(new Nothing());
            return res;
        }

        public Task<List<ConnectedRequestDto>> GetAllConnectedRequest(long id)
        {
            return _queryProcessor.ProcessAsync<FindByKey, List<ConnectedRequestDto>>(new FindByKey(id));
        }

        public Task<List<RequestGrid>> SearchRequest(RequestAutoCompleteDto dto)
        {
            RequestPagingContract pagingContract = new RequestPagingContract()
            {
                PageSize = 100,
                PageNumber = 1,
                Title = dto.Title.Recheck(),
                ExcludeRequestIds = dto.ExcludeRequestIds
            };
            return _queryProcessor.ProcessAsync<RequestPagingContract, List<RequestGrid>>(pagingContract);
        }


        public async Task<PagedListResult<AnnouncementRequestGrid>> GetAllAnnouncementRequest(PagingContract contract)
        {
            var list = await _queryProcessor.ProcessAsync<PagingContract, List<AnnouncementRequestGrid>>(contract);
            var count = await _queryProcessor.ProcessAsync<Nothing, AnnouncementRequestTotalCount>(new Nothing());
            return new PagedListResult<AnnouncementRequestGrid>()
            {
                List = list,
                TotalCount = count.Count,
                PageNumber = contract.PageNumber,
                PageSize = contract.PageSize,
            };
        }

        public async Task<List<RequestTargetAutocompleteResultDto>> SearchRequestTarget(RequestTargetAutoCompleteDto dto)
        {
            var pagingContract = new RequestTargetPagingContract()
            {
                PageSize = 100,
                PageNumber = 1,
                Title = dto.Title.Recheck(),
                ExcludeRequestTargetIds = dto.ExcludeRequestTargetIds
            };
            return (await _queryProcessor.ProcessAsync<RequestTargetPagingContract, List<RequestTargetGrid>>(pagingContract))
                   .Select(m => new RequestTargetAutocompleteResultDto(m.Id, m.Title)).ToList();
        }

        // public Task<List<RequestGrid>> GetAllRequestWithRequestTarget(RequestWithRequestTargetQuery query)
        // {
        //     return _queryProcessor.ProcessAsync<RequestWithRequestTargetQuery, List<RequestGrid>>(query);
        // }
    }
}