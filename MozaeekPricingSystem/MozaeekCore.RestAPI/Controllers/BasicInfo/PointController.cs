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
    public class PointController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IPointQueryFacade _pointQueryFacade;

        public PointController(ICommandBus commandBus, IPointQueryFacade pointQueryFacade)
        {
            _commandBus = commandBus;
            _pointQueryFacade = pointQueryFacade;
        }

        [HttpGet("{id}")]
        public Task<PointDto> GetById(long id)
        {
            return _pointQueryFacade.GetPointById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePointCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreatePointCommand, CreatePointCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet()]
        public Task<InitPointDto> GetInitPointDto()
        {
            return _pointQueryFacade.GetInitPointDto();
        }

        [HttpGet]
        public Task<PagedListResult<PointGrid>> GetAllParent([FromQuery] PointFilterContract pagingContract)
        {
            return _pointQueryFacade.GetAllParentPoints(pagingContract);
        }
        [HttpGet]
        public Task<PagedListResult<PointGrid>> GetAllChildren(long id)
        {
            return _pointQueryFacade.GetAllPointChildren(id);
        }
        [HttpGet]
        public Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = _commandBus.DispatchAsync<DeletePointCommand, DeleteCommandResult>(new DeletePointCommand(id));
            return commandResult;
        }
        
        [HttpPost]
        public Task<UpdatePointCommandResult> Update(UpdatePointCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdatePointCommand, UpdatePointCommandResult>(cmd);
            return commandResult;
        }
    }
}