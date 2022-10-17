using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Facade.Query;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Enum;
using MozaeekCore.RestAPI.Utility;
using MozaeekCore.ViewModel;

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
        
        #region PersonalInfo
        [HttpPost]
        public async Task<IActionResult> CreatePersonalInfo([FromForm] CreateTechnicianPersonalInfoViewModel vm, IFormFile photo)
        {
            var command = new CreateTechnicianPersonalInfoCommand()
            {
                IdentityNumber = vm.IdentityNumber,
                TechnicianType = vm.TechnicianType,
                LastName = vm.LastName,
                NationalCode = vm.NationalCode,
                FirstName = vm.FirstName
            };
            if (photo != null)
            {
                var attachment = photo.GetAttachmentDto(AttachmentType.PersonalPhoto);
                command.Attachment = attachment;
            }
            var commandResult = await commandBus.DispatchAsync<CreateTechnicianPersonalInfoCommand, RegisterTechnicianCommandResult>(command);
            return CreatedAtAction(nameof(GetTechnicianPersonalInfoById), new { id = commandResult.Id }, commandResult.Id);
        }
        [HttpGet("{id}")]
        public Task<TechnicianPersonalInfoDto> GetTechnicianPersonalInfoById(long id)
        {
            return _technicianQueryFacade.GetTechnicianPersonalInfoByTechnicianId(id);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonalInfo([FromForm] UpdateTechnicianPersonalInfoViewModel vm, IFormFile photo)
        {
            var command = new UpdateTechnicianPersonalInfoCommand()
            {
                TechnicianId = vm.TechnicianId,
                IdentityNumber = vm.IdentityNumber,
                TechnicianType = vm.TechnicianType,
                LastName = vm.LastName,
                NationalCode = vm.NationalCode,
                FirstName = vm.FirstName
            };
            if (photo != null)
            {
                var attachment = photo.GetAttachmentDto(AttachmentType.PersonalPhoto);
                command.Attachment = attachment;
            }

            var commandResult = await commandBus.DispatchAsync<UpdateTechnicianPersonalInfoCommand, RegisterTechnicianCommandResult>(command);
            return CreatedAtAction(nameof(GetTechnicianPersonalInfoById), new { id = commandResult.Id }, commandResult.Id);
        }

        #endregion
        #region Subject
        [HttpPost]
        public async Task<IActionResult> AddSubjects(AddingToTechnicianSubjectCommand cmd)
        {
            var commandResult = await commandBus.DispatchAsync<AddingToTechnicianSubjectCommand, RegisterTechnicianCommandResult>(cmd);
            return CreatedAtAction(nameof(GetTechnicianSubjectById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet("{id}")]
        public Task<TechnicianSubjectDto> GetTechnicianSubjectById(long id)
        {
            return _technicianQueryFacade.GetTechnicianSubjectByTechnicianId(id);
        }
        #endregion
        #region Point
        [HttpPost]
        public async Task<IActionResult> AddPoints(AddingToTechnicianPointCommand cmd)
        {
            var commandResult = await commandBus.DispatchAsync<AddingToTechnicianPointCommand, RegisterTechnicianCommandResult>(cmd);
            return CreatedAtAction(nameof(GetTechnicianPointById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet("{id}")]
        public Task<TechnicianPointDto> GetTechnicianPointById(long id)
        {
            return _technicianQueryFacade.GetTechnicianPointByTechnicianId(id);
        }
        #endregion
        #region ContactInfo
        [HttpPost]
        public async Task<IActionResult> AddContactInfo(AddingToTechnicianContactInfoCommand command)
        {
            var commandResult = await commandBus.DispatchAsync<AddingToTechnicianContactInfoCommand, RegisterTechnicianCommandResult>(command);
            return CreatedAtAction(nameof(GetTechnicianContactInfoById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet("{id}")]
        public Task<TechnicianContactInfoDto> GetTechnicianContactInfoById(long id)
        {
            return _technicianQueryFacade.GetTechnicianContactInfoByTechnicianId(id);
        }

        #endregion
        #region EducationalInfo
        [HttpGet("{id}")]
        public Task<TechnicianEducationalInfoDto> GetTechnicianEducationalInfoById(long id)
        {
            return _technicianQueryFacade.GetTechnicianEducationalInfoByTechnicianId(id);
        }

        [HttpGet]
        public Task<InitTechnicianEducationalInfo> GetInitTechnicianEducationalInfo()
        {
            return _technicianQueryFacade.GetInitTechnicianEducationalInfo();
        }

        [HttpPost]
        public async Task<IActionResult> AddEducationalInfo(AddingToTechnicianEducationalInfoCommand command)
        {
            var commandResult = await commandBus.DispatchAsync<AddingToTechnicianEducationalInfoCommand, RegisterTechnicianCommandResult>(command);
            return CreatedAtAction(nameof(GetTechnicianEducationalInfoById), new { id = commandResult.Id }, commandResult.Id);
        }
        #endregion
        #region Request
        [HttpPost]
        public async Task<IActionResult> AddRequests(AddingToTechnicianRequestCommand cmd)
        {
            var commandResult = await commandBus.DispatchAsync<AddingToTechnicianRequestCommand, RegisterTechnicianCommandResult>(cmd);
            return CreatedAtAction(nameof(GetTechnicianRequestById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet("{id}")]
        public Task<TechnicianRequestDto> GetTechnicianRequestById(long id)
        {
            return _technicianQueryFacade.GetTechnicianRequestByTechnicianId(id);
        }
        #endregion
        #region Attachment
        [HttpPost]
        public async Task<RegisterTechnicianCommandResult> AddAttachments([FromForm] TechnicianAttachmentViewModel vm, IFormFile file)
        {
            var attachment = file.GetAttachmentDto(vm.AttachmentType);
            var command = new AddingToTechnicianAttachmentCommand(vm.TechnicianId, attachment);

            return await commandBus.DispatchAsync<AddingToTechnicianAttachmentCommand, RegisterTechnicianCommandResult>(command);
            // return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }
        [HttpPost]
        public async Task<RegisterTechnicianCommandResult> RemoveAttachments(TechnicianAttachmentViewModel vm, IFormFile file)
        {
            var attachment = file.GetAttachmentDto(vm.AttachmentType);
            var command = new AddingToTechnicianAttachmentCommand(vm.TechnicianId, attachment);

            return await commandBus.DispatchAsync<AddingToTechnicianAttachmentCommand, RegisterTechnicianCommandResult>(command);
            //  return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }
        //[HttpGet]
        //public Task<TechnicianAttachmentDto> GetTechnicianAttachmentById(long id)
        //{
        //    return _technicianQueryFacade.GetTechnicianAttachmentByTechnicianId(id);
        //}
        #endregion
    }
}