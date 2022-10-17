using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Facade.Query;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RestAPI.Controllers.RSS
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = RoleNames.BasiInfo + "," + RoleNames.Admin)]
    public class RSSController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IRSSQueryFacade _rssQueryFacade;

        public RSSController(ICommandBus commandBus, IRSSQueryFacade rssQueryFacade)
        {
            _commandBus = commandBus;
            _rssQueryFacade = rssQueryFacade;
        }

        [HttpGet("{id}")]
        public Task<RSSDto> GetById(long id)
        {
            return _rssQueryFacade.GetRSSById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRSSCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateRSSCommand, CreateRSSCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet]
        public Task<PagedListResult<RSSDto>> GetAllParent([FromQuery] PagingContract pagingContract)
        {
            return _rssQueryFacade.GetAllRSSs(pagingContract);
        }

        [HttpGet]
        public Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = _commandBus.DispatchAsync<DeleteRSSCommand, DeleteCommandResult>(new DeleteRSSCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateRSSCommandResult> Update(UpdateRSSCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdateRSSCommand, UpdateRSSCommandResult>(cmd);
            return commandResult;
        }

    }
}
