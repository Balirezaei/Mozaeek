using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.File;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Enum;
using MozaeekCore.Facade.Contract;
using MozaeekCore.Facade.Query;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = RoleNames.BasiInfo + "," + RoleNames.Admin)]
    public class RequestOrgController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IRequestOrgQueryFacade _requestOrgQueryFacade;

        public RequestOrgController(ICommandBus commandBus, IRequestOrgQueryFacade requestOrgQueryFacade)
        {
            this._commandBus = commandBus;
            _requestOrgQueryFacade = requestOrgQueryFacade;
        }

        [HttpGet("{id}")]
        public Task<RequestOrgDto> GetById(long id)
        {
            return _requestOrgQueryFacade.GetRequestOrgById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestOrgCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateRequestOrgCommand, CreateRequestOrgCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet]
        public Task<PagedListResult<RequestOrgGrid>> GetAllParent([FromQuery] RequestOrgFilterContract pagingContract)
        {
            return _requestOrgQueryFacade.GetAllParentRequestOrgs(pagingContract);
        }

        [HttpGet]
        public Task<PagedListResult<RequestOrgGrid>> GetAllChildren(long id)
        {
            return _requestOrgQueryFacade.GetAllRequestOrgChildren(id);
        }

        [HttpGet("")]
        public Task<InitRequestOrgDto> GetInitRequestOrgDto()
        {
            return _requestOrgQueryFacade.GetInitRequestOrgDto();
        }


        [HttpGet("{id}")]
        public Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = _commandBus.DispatchAsync<DeleteRequestOrgCommand, DeleteCommandResult>(new DeleteRequestOrgCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateRequestOrgCommandResult> Update(UpdateRequestOrgCommand cmd)
        {
            var commandResult = _commandBus.DispatchAsync<UpdateRequestOrgCommand, UpdateRequestOrgCommandResult>(cmd);
            return commandResult;
        }

        [HttpGet]
        public Task<List<SynonymsDto>> GetAllSynonym()
        {
            return _requestOrgQueryFacade.GetAllSynonym();
        }
        [HttpPost]
        public Task<CreateSynonymsCommandResult> CreateSynonym(RequestOrgSynonymDto dto)
        {
            var cmd = new CreateSynonymsCommand()
            { EntityType = EntityType.RequestOrg, Synonym = dto.Synonym, Title = dto.Title };
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
            await _commandBus.DispatchAsync<CreateRequestOrgFromExcelCommand, List<RequestOrgDto>>(new CreateRequestOrgFromExcelCommand() { ExcelPath = commandFileResult.Path });
        }

        [HttpPost]
        public Task<CreateDefiniteRequestOrgCommandResult> CreateDefiniteRequestOrg(CreateDefiniteRequestOrgCommand cmd)
        {
            return _commandBus.DispatchAsync<CreateDefiniteRequestOrgCommand, CreateDefiniteRequestOrgCommandResult>(cmd);
        }
        [HttpGet]
        public Task<Nothing> RemoveDefiniteRequestOrg(long id)
        {
            return _commandBus.DispatchAsync<DeleteDefiniteRequestOrgCommand, Nothing>(new DeleteDefiniteRequestOrgCommand(){Id = id});
        }
        [HttpPost]
        public Task<UpdateDefiniteRequestOrgCommandResult> UpdateDefiniteRequestOrg(UpdateDefiniteRequestOrgCommand cmd)
        {
            return _commandBus.DispatchAsync<UpdateDefiniteRequestOrgCommand, UpdateDefiniteRequestOrgCommandResult>(cmd);
        }

        [HttpGet]
        public Task<DefiniteRequestOrgDto> GetDefiniteRequestOrdById(long id)
        {
            return _requestOrgQueryFacade.GetDefiniteRequestOrdById(id);
        }

        [HttpGet]
        public Task<List<DefiniteRequestOrgDto>> GetAllDefiniteRequestOrdById(long requestOrgId)
        {
            return _requestOrgQueryFacade.GetAllDefiniteRequestOrdById(requestOrgId);
        }

        [HttpGet("")]
        public Task<InitDefiniteRequestOrg> GetInitDefiniteRequestOrg()
        {
            return _requestOrgQueryFacade.GetInitDefiniteRequestOrg();
        }
    }
}