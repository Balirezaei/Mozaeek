using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Enum;
using MozaeekCore.Facade.Contract;
using MozaeekCore.Facade.Query;
using MozaeekCore.QueryModel;
using RequestActDto = MozaeekCore.ApplicationService.Contract.RequestActDto;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = RoleNames.BasiInfo + "," + RoleNames.Admin)]
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
        public Task<RequestActDto> GetById(long id)
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

        [HttpGet("{id}")]
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

        [HttpGet]
        public Task<List<SynonymsDto>> GetAllSynonym()
        {
            return _requestActQueryFacade.GetAllSynonym();
        }
        [HttpPost]
        public Task<CreateSynonymsCommandResult> CreateSynonym(RequestActSynonymDto dto)
        {
            var cmd = new CreateSynonymsCommand()
            { EntityType = EntityType.RequestAct, Synonym = dto.Synonym, Title = dto.Title };
            return _commandBus.DispatchAsync<CreateSynonymsCommand, CreateSynonymsCommandResult>(cmd);
        }
        [HttpGet("{id}")]
        public Task<Nothing> DeleteSynonym(long id)
        {
            return _commandBus.DispatchAsync<DeleteSynonymsCommand, Nothing>(new DeleteSynonymsCommand(id));
        }
    }
}