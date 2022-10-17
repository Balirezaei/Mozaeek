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
    public class SubjectPriceController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ISubjectPriceQueryFacade _subjectPriceQueryFacade;

        public SubjectPriceController(ICommandBus commandBus, ISubjectPriceQueryFacade subjectPriceQueryFacade)
        {
            this._commandBus = commandBus;
            _subjectPriceQueryFacade = subjectPriceQueryFacade;
        }


        [HttpGet("{id}")]
        public Task<SubjectPriceDto> GetById(long id)
        {
            return _subjectPriceQueryFacade.GetSubjectPriceById(id);
        }

        [HttpGet]
        public Task<PagedListResult<SubjectPriceGrid>> GetAll([FromQuery] PagingContract pagingContract)
        {
            return _subjectPriceQueryFacade.GetAllSubjectPrices(pagingContract);
        }
        
        [HttpGet("")]
        public Task<InitSubjectPriceDto> GetInitSubjectPriceDto()
        {
            return _subjectPriceQueryFacade.GetInitSubjectPriceDto();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubjectPriceCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateSubjectPriceCommand, CreateSubjectPriceResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet]
        public async Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = await _commandBus.DispatchAsync<DeleteSubjectPriceCommand, DeleteCommandResult>(new DeleteSubjectPriceCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateSubjectPriceResult> Update(UpdateSubjectPriceCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdateSubjectPriceCommand, UpdateSubjectPriceResult>(cmd);
            return commandResult;
        }
    }
}