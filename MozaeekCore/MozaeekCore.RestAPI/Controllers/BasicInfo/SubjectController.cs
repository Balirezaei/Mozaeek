using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.File;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Facade.Contract;
using MozaeekCore.Facade.Query;
using MozaeekCore.Enum;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = RoleNames.BasiInfo + "," + RoleNames.Admin)]
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

        [HttpGet("{id}")]
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

        [HttpGet]
        public Task<List<SynonymsDto>> GetAllSynonym()
        {
            return _subjectQueryFacade.GetAllSynonym();
        }
        [HttpPost]
        public Task<CreateSynonymsCommandResult> CreateSynonym(SubjectSynonymDto dto)
        {
            var cmd = new CreateSynonymsCommand()
            { EntityType = EntityType.Subject, Synonym = dto.Synonym, Title = dto.Title };
            return _commandBus.DispatchAsync<CreateSynonymsCommand, CreateSynonymsCommandResult>(cmd);
        }
        [HttpGet("{id}")]
        public Task<Nothing> DeleteSynonym(long id)
        {
            return _commandBus.DispatchAsync<DeleteSynonymsCommand, Nothing>(new DeleteSynonymsCommand(id));
        }

        [HttpPost]
        public async Task ImportFromExcel(IFormFile file)
        {
            var commandFile = new CreateFileCommand() { File = file, Type = FileType.ExcelDataImport };
            var commandFileResult = await _commandBus.DispatchAsync<CreateFileCommand, CreateFileCommandResult>(commandFile);
            await _commandBus.DispatchAsync<CreateSubjectFromExcelCommand, List<SubjectDto>>(new CreateSubjectFromExcelCommand() { ExcelPath = commandFileResult.Path });
        }
    }
}
