using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MozaeekCore.ApplicationService.Contract.File;
using MozaeekCore.Core.CommandBus;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class MosaikFileController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public MosaikFileController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpPost]
        public Task<CreateFileCommandResult> UploadQuestionAttachment([FromForm] QuestionAttachmentDto dto)
        {
            var commandFile = new CreateFileCommand() { File = dto.File, Type = FileType.QuestionAttachment };
            return _commandBus.DispatchAsync<CreateFileCommand, CreateFileCommandResult>(commandFile);
        }

        [HttpPost]
        public Task<CreateFileCommandResult> UploadQuestionVoice([FromForm] QuestionAttachmentDto dto)
        {
            var commandFile = new CreateFileCommand() { File = dto.File, Type = FileType.QuestionVoice };
            return _commandBus.DispatchAsync<CreateFileCommand, CreateFileCommandResult>(commandFile);
        }

        [HttpPost]
        public Task<CreateFileCommandResult> UploadFile([FromForm] FileDto dto)
        {
            var commandFile = new CreateFileCommand() { File = dto.File, Type = dto.Type };
            return _commandBus.DispatchAsync<CreateFileCommand, CreateFileCommandResult>(commandFile);
        }
    }

}
