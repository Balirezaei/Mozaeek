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

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ISubjectQueryFacade _subjectQueryFacade;

        public SubjectController(ICommandBus commandBus, ISubjectQueryFacade subjectQueryFacade)
        {
            this._commandBus = commandBus;
            _subjectQueryFacade = subjectQueryFacade;
        }


        [HttpGet("{id}")]
        public Task<SubjectDto> GetById(long id)
        {
            return _subjectQueryFacade.GetSubjectById(id);
        }

        [HttpGet]
        public Task<PagedListResult<SubjectGrid>> GetAllParent([FromQuery] SubjectFilterContract pagingContract)
        {
            return _subjectQueryFacade.GetAllParentSubjects(pagingContract);
        }

        [HttpGet]
        public Task<PagedListResult<SubjectGrid>> GetAllChildren(long id)
        {
            return _subjectQueryFacade.GetAllSubjectChildren(id);
        }

        [HttpGet("")]
        public Task<InitSubjectDto> GetInitSubjectDto()
        {
            return _subjectQueryFacade.GetInitSubjectDto();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubjectCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateSubjectCommand, CreateSubjectCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet]
        public Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = _commandBus.DispatchAsync<DeleteSubjectCommand, DeleteCommandResult>(new DeleteSubjectCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateSubjectCommandResult> Update(UpdateSubjectCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdateSubjectCommand, UpdateSubjectCommandResult>(cmd);
            return commandResult;
        }
    }
}
