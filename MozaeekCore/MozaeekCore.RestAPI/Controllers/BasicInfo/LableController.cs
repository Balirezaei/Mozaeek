using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.BasicInfo.Point;
using MozaeekCore.ApplicationService.Contract.File;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Enum;
using MozaeekCore.Facade.Contract;
using MozaeekCore.Facade.Query;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = RoleNames.BasiInfo + "," + RoleNames.Admin)]
    public class LabelController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ILabelQueryFacade _labelQueryFacade;

        public LabelController(ICommandBus commandBus, ILabelQueryFacade labelQueryFacade)
        {
            this._commandBus = commandBus;
            _labelQueryFacade = labelQueryFacade;
        }


        [HttpGet("{id}")]
        public Task<LabelDto> GetById(long id)
        {
            return _labelQueryFacade.GetLabelById(id);
        }

        [HttpGet]
        public Task<PagedListResult<LabelGrid>> GetAllParent([FromQuery] LabelFilterContract pagingContract)
        {
            return _labelQueryFacade.GetAllParentLabels(pagingContract);
        }

        [HttpGet]
        public Task<PagedListResult<LabelGrid>> GetAllChildren(long id)
        {
            return _labelQueryFacade.GetAllLabelChildren(id);
        }

        [HttpGet("")]
        public Task<InitLabelDto> GetInitLabelDto()
        {
            return _labelQueryFacade.GetInitLabelDto();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLabelCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateLabelCommand, CreateLabelCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet]
        public async Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = await _commandBus.DispatchAsync<DeleteLabelCommand, DeleteCommandResult>(new DeleteLabelCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateLabelCommandResult> Update(UpdateLabelCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdateLabelCommand, UpdateLabelCommandResult>(cmd);
            return commandResult;
        }

        [HttpGet]
        public Task<List<SynonymsDto>> GetAllSynonym()
        {
            return _labelQueryFacade.GetAllSynonym();
        }

        [HttpPost]
        public Task<CreateSynonymsCommandResult> CreateSynonym(LabelSynonymDto dto)
        {
            var cmd = new CreateSynonymsCommand()
            { EntityType = EntityType.Label, Synonym = dto.Synonym, Title = dto.Title };
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
            await _commandBus.DispatchAsync<CreateLabelFromExcelCommand, List<LabelDto>>(new CreateLabelFromExcelCommand() { ExcelPath = commandFileResult.Path });
        }
    }
}
