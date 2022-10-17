using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Facade.Contract;
using MozaeekCore.Facade.Query;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = RoleNames.Operation + "," + RoleNames.Admin)]
    public class RequestController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IRequestQueryFacade _facade;

        public RequestController(ICommandBus commandBus, IRequestQueryFacade facade)
        {
            _commandBus = commandBus;
            _facade = facade;
        }

        [HttpGet("{id}")]
        public Task<RequestDto> GetById(long id)
        {
            return _facade.GetRequestById(id);
        }

        [HttpGet]
        public Task<PagedListResult<RequestGrid>> GetAll([FromQuery] RequestFilterDto pagingContract)
        {
            return _facade.GetAllRequests(pagingContract);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestCommand _command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateRequestCommand, CreateRequestCommandResult>(_command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet()]
        public Task<InitRequestDto> GetInitRequestDto()
        {
            return _facade.GetInitRequestDto();
        }

        [HttpGet]
        public Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = _commandBus.DispatchAsync<DeleteRequestCommand, DeleteCommandResult>(new DeleteRequestCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateRequestCommandResult> Update(UpdateRequestCommand cmd)
        {
            var commandResult =
                _commandBus.DispatchAsync<UpdateRequestCommand, UpdateRequestCommandResult>(cmd);
            return commandResult;
        }


        [HttpPost]
        public Task<List<RequestGrid>> Search(RequestAutoCompleteDto dto)
        {
            return _facade.SearchRequest(dto);
        }

        [HttpPost]
        public Task<List<RequestTargetAutocompleteResultDto>> SearchReqestTarget(RequestTargetAutoCompleteDto dto)
        {
            return _facade.SearchRequestTarget(dto);
        }


        [HttpGet]
        public Task<PagedListResult<AnnouncementRequestGrid>> GetAllNewsRequest([FromQuery] PagingContract pagingContract)
        {
            return _facade.GetAllAnnouncementRequest(pagingContract);
        }

        [HttpPost]
        public Task<Nothing> NewsRequestUpdateRequest(AnnouncementRequestAssignRequestCommand command)
        {
            var commandResult =
                _commandBus.DispatchAsync<AnnouncementRequestAssignRequestCommand, Nothing>(command);
            return commandResult;
        }

        [HttpGet]
        public Task<List<RequestGrid>> GetRequestByRequestTarget([FromQuery] RequestWithRequestTargetQuery query)
        {
            return _facade.GetAllRequestsByRequestTarget(query.RequestTargetId);
        }


        [HttpPost]
        public async Task<IActionResult> CreateWithAnnouncement(CreateRequestWithAnnouncementCommand _command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateRequestWithAnnouncementCommand, CreateRequestCommandResult>(_command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }


    }
}
