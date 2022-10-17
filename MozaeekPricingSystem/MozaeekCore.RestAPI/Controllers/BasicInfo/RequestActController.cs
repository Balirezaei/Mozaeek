using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Facade.Query;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RequestActController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IRequestActQueryFacade _requestActQueryFacade;

        public RequestActController(ICommandBus commandBus, IRequestActQueryFacade requestActQueryFacade)
        {
            this._commandBus = commandBus;
            _requestActQueryFacade = requestActQueryFacade;
        }

        [HttpGet("{id}")]
        public  Task<RequestActDto> GetById(long id)
        {
            return _requestActQueryFacade.GetRequestActById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestActCommand actCommand)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateRequestActCommand, CreateRequestActCommandResult>(actCommand);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }


        [HttpGet]
        public Task<PagedListResult<RequestActDto>> GetAll([FromQuery] PagingContract pagingContract)
        {
            return _requestActQueryFacade.GetAllRequestActs(pagingContract);
        }

        [HttpGet]
        public Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = _commandBus.DispatchAsync<DeleteRequestActCommand, DeleteCommandResult>(new DeleteRequestActCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<RequestActRequestCommandResult> Update(UpdateRequestActCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdateRequestActCommand, RequestActRequestCommandResult>(cmd);
            return commandResult;
        }
    }
}