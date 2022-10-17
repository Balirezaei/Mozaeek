using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Facade.Query;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
        public Task<PagedListResult<RequestGrid>> GetAll([FromQuery] PagingContract pagingContract)
        {
            return _facade.GetAllRequests(pagingContract);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestCommand _command)
        {
            var commandResult= await _commandBus.DispatchAsync<CreateRequestCommand, CreateRequestCommandResult>(_command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet()]
        public Task<InitRequestDto> GetInitLabelDto()
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

    }
}
