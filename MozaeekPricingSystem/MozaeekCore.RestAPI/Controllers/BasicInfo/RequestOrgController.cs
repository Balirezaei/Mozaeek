using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Facade.Query;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RequestOrgController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IRequestOrgQueryFacade _requestOrgQueryFacade;

        public RequestOrgController(ICommandBus commandBus, IRequestOrgQueryFacade requestOrgQueryFacade)
        {
            this._commandBus = commandBus;
            _requestOrgQueryFacade = requestOrgQueryFacade;
        }

        [HttpGet("{id}")]
        public Task<RequestOrgDto> GetById(long id)
        {
            return _requestOrgQueryFacade.GetRequestOrgById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestOrgCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateRequestOrgCommand, CreateRequestOrgCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet]
        public Task<PagedListResult<RequestOrgGrid>> GetAllParent([FromQuery] RequestOrgFilterContract pagingContract)
        {
            return _requestOrgQueryFacade.GetAllParentRequestOrgs(pagingContract);
        }

        [HttpGet]
        public Task<PagedListResult<RequestOrgGrid>> GetAllChildren(long id)
        {
            return _requestOrgQueryFacade.GetAllRequestOrgChildren(id);
        }

        [HttpGet("")]
        public Task<InitRequestOrgDto> GetInitRequestOrgDto()
        {
            return _requestOrgQueryFacade.GetInitRequestOrgDto();
        }
 

        [HttpGet]
        public Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = _commandBus.DispatchAsync<DeleteRequestOrgCommand, DeleteCommandResult>(new DeleteRequestOrgCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateRequestOrgCommandResult> Update(UpdateRequestOrgCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdateRequestOrgCommand, UpdateRequestOrgCommandResult>(cmd);
            return commandResult;
        }
    }
}