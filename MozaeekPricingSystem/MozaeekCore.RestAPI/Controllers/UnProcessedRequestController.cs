using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Facade.Query;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class UnProcessedRequestController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        private readonly IUnProcessedRequestQueryFacade _processedRequestQueryFacade;

        public UnProcessedRequestController(ICommandBus commandBus, IUnProcessedRequestQueryFacade processedRequestQueryFacade)
        {
            _commandBus = commandBus;
            _processedRequestQueryFacade = processedRequestQueryFacade;
        }
        
        [HttpGet]
        public Task<UnProcessedRequestDto> GetById(int id)
        {
            return _processedRequestQueryFacade.GetProcessedRequestById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUnProcessedRequestCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateUnProcessedRequestCommand, UnProcessedRequestCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

    }
}
