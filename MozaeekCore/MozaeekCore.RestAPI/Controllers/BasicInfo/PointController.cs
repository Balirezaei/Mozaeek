using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.BasicInfo.Point;
using MozaeekCore.ApplicationService.Contract.File;
using MozaeekCore.Core.Base;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Facade.Contract;
using MozaeekCore.Facade.Query;

namespace MozaeekCore.RestAPI.Controllers.BasicInfo
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = RoleNames.BasiInfo + "," + RoleNames.Admin)]
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
        public Task<PagedListResult<PointGrid>> GetAllParent([FromQuery] PointFilterDto filterDto)
        {
            return _pointQueryFacade.GetAllParentPoints(filterDto);
        }
        [HttpGet]
        public Task<PagedListResult<PointGrid>> GetAllChildren(long id)
        {
            return _pointQueryFacade.GetAllPointChildren(id);
        }
        [HttpGet("")]
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
        [HttpPost]
        public async Task<MozaeekCore.Core.ResponseMessages.Result> ImportFromExcel (IFormFile file)
        {
            var commandFile = new CreateFileCommand() { File = file, Type = FileType.ExcelDataImport };
            var commandFileResult = await _commandBus.DispatchAsync<CreateFileCommand, CreateFileCommandResult>(commandFile);
            await _commandBus.DispatchAsync<CreatePointFromExcelCommand, List<CreatePointCommandResult>>(new CreatePointFromExcelCommand() {ExcelPath=commandFileResult.Path });
            return new Result() { Data = "OK" };
        }
        [HttpPost]
        public async Task<MozaeekCore.Core.ResponseMessages.Result> DeletePoints()
        {

                var commandFileResult = await _commandBus.DispatchAsync<Command, DeleteCommandResult>(new Command());
                // await _commandBus.DispatchAsync<CreatePointFromExcelCommand, List<CreatePointCommandResult>>(new CreatePointFromExcelCommand() { ExcelPath = commandFileResult.Path });
                return new Result() { Data="OK"};
       
        }
    }
}