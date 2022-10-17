using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Facade.Query;
using MozaeekCore.QueryModel;
using MozaeekCore.RestAPI.Utility;
using MozaeekCore.ViewModel;
using MozaeekCore.ApplicationService.Contract.File;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = RoleNames.Operation + "," + RoleNames.Admin)]
    public class AnnouncementController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IAnnouncementQueryFacade _facade;
        private readonly IRequestQueryFacade _requestfacade;

        public AnnouncementController(ICommandBus commandBus, IAnnouncementQueryFacade facade, IRequestQueryFacade requestfacade)
        {
            _commandBus = commandBus;
            _facade = facade;
            _requestfacade = requestfacade;
        }

        [HttpGet]
        public Task<PagedListResult<NewsForProcessGrid>> GetAllNewsReadyToProcess([FromQuery] PagingContract pagingContract)
        {
            return _facade.GetAllNewsReadyToProcess(pagingContract);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAnnouncementViewModel vm, IFormFile photo)
        {
            var commandFileResult = new CreateFileCommandResult();
            if (photo != null)
            {
                var commandFile = new CreateFileCommand() { File = photo, Type = FileType.AnnouncementPhoto };
                commandFileResult = await _commandBus.DispatchAsync<CreateFileCommand, CreateFileCommandResult>(commandFile);
            }
            var command = new CreateAnnouncementCommand()
            {
                Title = vm.Title,
                RequestOrgs = vm.RequestOrgs,
                Subjects = vm.Subjects,
                Labels = vm.Labels,
                Points = vm.Points,
                Description = vm.Description,
                NewsId = vm.NewsId,
                Summary = vm.Summary,
                FileId = commandFileResult?.Id,
                Url = commandFileResult?.Url,
                HasRequest = vm.HasRequest
            };

            var commandResult = await _commandBus.DispatchAsync<CreateAnnouncementCommand, CreateAnnouncementCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet("{id}")]
        public Task<AnnouncementDto> GetById(long id)
        {
            return _facade.GetAnnouncementById(id);
        }
        [HttpGet("{id}")]
        public Task<NewsForProcess> GetRssNewsById(long id)
        {
            return _facade.GetRssNewsById(id);
        }


        [HttpGet]
        public Task<PagedListResult<AnnouncementGrid>> GetAll([FromQuery] PagingContract pagingContract)
        {
            return _facade.GetAllAnnouncements(pagingContract);
        }

        [HttpGet()]
        public Task<InitAnnouncementDto> GetInitAnnouncementDto()
        {
            return _facade.GetInitAnnouncementDto();
        }

        [HttpGet]
        public Task<DeleteCommandResult> Delete(long id)
        {
            var commandResult = _commandBus.DispatchAsync<DeleteAnnouncementCommand, DeleteCommandResult>(new DeleteAnnouncementCommand(id));
            return commandResult;
        }

        [HttpPost]
        public Task<UpdateAnnouncementCommandResult> Update([FromForm] UpdateAnnouncementViewModel vm, IFormFile? photo)
        {
            var commandFileResult = new CreateFileCommandResult();
            if (photo != null)
            {
                var commandFile = new CreateFileCommand() { File = photo, Type = FileType.AnnouncementPhoto };
                commandFileResult = _commandBus.DispatchAsync<CreateFileCommand, CreateFileCommandResult>(commandFile).Result;
            }
            var command = new UpdateAnnouncementCommand()
            {
                Title = vm.Title,
                Description = vm.Description,
                RequestOrgs = vm.RequestOrgs,
                Subjects = vm.Subjects,
                Labels = vm.Labels,
                Points = vm.Points,
                Id = vm.Id,
                Summary = vm.Summary,
                FileId = commandFileResult.Id,
                Url = commandFileResult.Url
            };

            var commandResult = _commandBus.DispatchAsync<UpdateAnnouncementCommand, UpdateAnnouncementCommandResult>(command);
            return commandResult;
        }

        [HttpGet("{id}")]
        public Task<Nothing> RssNewsChangeState(long id)
        {
            var commandResult =
             _commandBus.DispatchAsync<RssNewsChangeIsProcessCommand, Nothing>(new RssNewsChangeIsProcessCommand() { NewsId = id });
            return commandResult;
        }
        [HttpGet]
        public Task<PagedListResult<AnnouncementRequestGrid>> GetAllAnnouncementRequest([FromQuery] PagingContract pagingContract)
        {
            return _facade.GetAllAnnouncementRequests(pagingContract);
        }

        [HttpGet]
        public Task<List<RequestGrid>> GetRequestByRequestTarget([FromQuery] RequestWithRequestTargetQuery query)
        {
            return _requestfacade.GetAllRequestsByRequestTarget(query.RequestTargetId);
        }

        //
        // [HttpGet("{id}")]
        // public Task<Nothing> MoveToRequestNews(long id)
        // {
        //     var commandResult =
        //         _commandBus.DispatchAsync<RssNewsChangeIsRequestCommand, Nothing>(new RssNewsChangeIsRequestCommand() { NewsId = id });
        //     return commandResult;
        // }

    }
}
