// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using MozaeekCore.ApplicationService.Contract;
// using MozaeekCore.Core.CommandBus;
// using MozaeekCore.Core.ResponseMessages;
// using MozaeekCore.Facade.Query;
// using MozaeekCore.QueryModel;
//
// namespace MozaeekCore.RestAPI.Controllers
// {
//     [Route("api/[controller]/[action]")]
//     [ApiController]
//     public class PreRequestController : ControllerBase
//     {
//         private readonly ICommandBus _commandBus;
//
//         private readonly IPreRequestQueryFacade _preRequestQueryFacade;
//
//         public PreRequestController(ICommandBus commandBus, IPreRequestQueryFacade preRequestQueryFacade)
//         {
//             _commandBus = commandBus;
//             _preRequestQueryFacade = preRequestQueryFacade;
//         }
//         
//         [HttpPost]
//         public async Task<IActionResult> Create(CreatePreRequestCommand command)
//         {
//             var commandResult = await _commandBus.DispatchAsync<CreatePreRequestCommand, CreatePreRequestCommandResult>(command);
//             return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
//         }
//
//         [HttpGet("{id}")]
//         public Task<PreRequestDto> GetById(long id)
//         {
//             return _preRequestQueryFacade.GetPreRequestById(id);
//         }
//
//         [HttpGet]
//         public Task<PagedListResult<PreRequestGrid>> GetAll([FromQuery] PagingContract pagingContract)
//         {
//             return _preRequestQueryFacade.GetAllPreRequests(pagingContract);
//         }
//         
//         [HttpGet]
//         public async Task<DeleteCommandResult> Delete(long id)
//         {
//             var commandResult = await _commandBus.DispatchAsync<PreRequestDeleteCommand, DeleteCommandResult>(new PreRequestDeleteCommand(id));
//             return commandResult;
//         }
//
//         [HttpPost]
//         public Task<UpdatePreRequestCommandResult> Update(UpdatePreRequestCommand cmd)
//         {
//             var commandResult = _commandBus.DispatchAsync<UpdatePreRequestCommand, UpdatePreRequestCommandResult>(cmd);
//             return commandResult;
//         }
//     }
// }
