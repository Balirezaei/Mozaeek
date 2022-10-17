using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Facade.Query;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Enum;
using MozaeekCore.QueryModel;
using MozaeekCore.RestAPI.Utility;
using MozaeekCore.ViewModel;
using MozaeekCore.ApplicationService.Contract.File;
using MozaeekCore.ApplicationService.Contract.Technician;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly ICommandBus commandBus;
        private readonly ITechnicianQueryFacade _technicianQueryFacade;

        public TechnicianController(ICommandBus commandBus, ITechnicianQueryFacade technicianQueryFacade)
        {
            this.commandBus = commandBus;
            this._technicianQueryFacade = technicianQueryFacade;
        }
  
        [HttpPost]
        public async Task<RegisterTechnicianCommandResult> CreateAgent([FromBody] TechnicianAgentViewModel input)
        {
            var command = new CreateAgentTechnicianCommand() { Address = input.Address, Email = input.Email, FileIds = input.FileIds, FirstName = input.FirstName, LastName = input.LastName, NationalId = input.NationalId, PhoneNmuber = input.PhoneNmuber, PointId = input.PointId, PostalCode = input.PostalCode };
            return await commandBus.DispatchAsync<CreateAgentTechnicianCommand, RegisterTechnicianCommandResult>(command);
         
        }

        [HttpPost]
        public async Task<RegisterTechnicianCommandResult> CreateGuid([FromBody] TechnicianGuidViewModel input)
        {
            var command = new CreateGuidTechnicianCommand() {  FileIds = input.FileIds, FirstName = input.FirstName, LastName = input.LastName, NationalId = input.NationalId, PhoneNmuber = input.PhoneNmuber, SubjectIds=input.SubjectIds};
            return await commandBus.DispatchAsync<CreateGuidTechnicianCommand, RegisterTechnicianCommandResult>(command);
        }

        [HttpPost]
        public async Task<RegisterTechnicianCommandResult> CreateExpert([FromBody] TechnicianExpertViewModel input)
        {
            var command = new CreateExpertTechnicianCommand() { FileIds = input.FileIds, FirstName = input.FirstName, LastName = input.LastName, NationalId = input.NationalId, PhoneNmuber = input.PhoneNmuber, DefiniteRequestOrgIds=input.DefiniteRequestOrgIds,PointId=input.PointId,RequestTargetIds=input.RequestTargetIds };
            return await commandBus.DispatchAsync<CreateExpertTechnicianCommand, RegisterTechnicianCommandResult>(command);
        }

        [HttpGet]
        public async Task<PagedListResult<TechnicianDto>> GetAll()
        {
            return await _technicianQueryFacade.GetAll();
        }

        [HttpPost]
        public async Task VerfyFirstStep(long id)
        {
             await commandBus.DispatchAsync<UpdateTechnicianVerificationStepCommand, UpdateTechnicianCommandResult>(new UpdateTechnicianVerificationStepCommand() {Id=id,isFirstStepVerified=true});
        }
        [HttpPost]
        public async Task VerfySecondStep(long id)
        {
            await commandBus.DispatchAsync<UpdateTechnicianVerificationStepCommand, UpdateTechnicianCommandResult>(new UpdateTechnicianVerificationStepCommand() { Id = id, isSecondStepVefied = true });
        }

    }
}