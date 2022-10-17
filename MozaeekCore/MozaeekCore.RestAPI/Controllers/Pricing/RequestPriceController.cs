using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Facade.Query;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RestAPI.Controllers.Pricing
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = RoleNames.Operation + "," + RoleNames.Admin)]
    public class RequestPriceController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IRequestPriceQueryFacade _requestPriceQueryFacade;

        public RequestPriceController(ICommandBus commandBus, IRequestPriceQueryFacade requestPriceQueryFacade)
        {
            this._commandBus = commandBus;
            _requestPriceQueryFacade = requestPriceQueryFacade;
        }


        [HttpGet("{id}")]
        public Task<RequestPriceDto> GetById(long id)
        {
            return _requestPriceQueryFacade.GetRequestPriceById(id);
        }

        [HttpGet]
        public Task<PagedListResult<RequestPriceGrid>> GetAll([FromQuery] PagingContract pagingContract)
        {
            return _requestPriceQueryFacade.GetAllRequestPrices(pagingContract);
        }

        [HttpGet("")]
        public Task<InitRequestPriceDto> GetInitRequestPriceDto()
        {
            return _requestPriceQueryFacade.GetInitRequestPriceDto();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestPriceCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateRequestPriceCommand, CreateRequestPriceResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet]
        public async Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = await _commandBus.DispatchAsync<DeleteRequestPriceCommand, DeleteCommandResult>(new DeleteRequestPriceCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateRequestPriceResult> Update(UpdateRequestPriceCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdateRequestPriceCommand, UpdateRequestPriceResult>(cmd);
            return commandResult;
        }

    }
}