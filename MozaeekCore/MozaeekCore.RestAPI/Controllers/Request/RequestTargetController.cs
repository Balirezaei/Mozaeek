using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Facade.Contract;
using MozaeekCore.Facade.Query;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = RoleNames.Operation + "," + RoleNames.Admin)]
    public class RequestTargetController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IRequestTargetQueryFacade _requestTargetQueryFacade;

        public RequestTargetController(ICommandBus commandBus, IRequestTargetQueryFacade requestTargetQueryFacade)
        {
            this._commandBus = commandBus;
            _requestTargetQueryFacade = requestTargetQueryFacade;
        }

        [HttpGet("{id}")]
        public Task<RequestTargetDto> GetById(long id)
        {
            return _requestTargetQueryFacade.GetRequestTargetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestTargetCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateRequestTargetCommand, CreateRequestTargetCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet]
        public Task<PagedListResult<RequestTargetGrid>> GetAll([FromQuery] RequestTargetFilterDto pagingContract)
        {
            return _requestTargetQueryFacade.GetAllRequestTargets(pagingContract);
        }

        [HttpGet]
        public Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = _commandBus.DispatchAsync<DeleteRequestTargetCommand, DeleteCommandResult>(new DeleteRequestTargetCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateRequestTargetCommandResult> Update(UpdateRequestTargetCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdateRequestTargetCommand, UpdateRequestTargetCommandResult>(cmd);
            return commandResult;
        }

        [HttpGet("")]
        public Task<InitRequestTargetDto> GetInitRequestTargetDto()
        {
            return _requestTargetQueryFacade.GetInitRequestTargetDto(null);
        }
    }
}