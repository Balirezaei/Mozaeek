using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.CommandBus;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Facade.Query;
using MozaeekCore.QueryModel;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IAnnouncementQueryFacade _facade;

        public AnnouncementController(ICommandBus commandBus, IAnnouncementQueryFacade facade)
        {
            _commandBus = commandBus;
            _facade = facade;
        }

        [HttpGet]
        public Task<List<NewsForProcess>> GetAllNewsReadyToProcess([FromQuery] PagingContract pagingContract)
        {
            return _facade.GetAllNewsReadyToProcess(pagingContract);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementCommand command)
        {
            var commandResult = await _commandBus.DispatchAsync<CreateAnnouncementCommand, CreateAnnouncementCommandResult>(command);
            return CreatedAtAction(nameof(GetById), new { id = commandResult.Id }, commandResult.Id);
        }

        [HttpGet("{id}")]
        public Task<AnnouncementDto> GetById(long id)
        {
            return _facade.GetAnnouncementById(id);
        }

        [HttpGet]
        public Task<PagedListResult<AnnouncementGrid>> GetAll([FromQuery] PagingContract pagingContract)
        {
            return _facade.GetAllAnnouncements(pagingContract);
        }

        [HttpGet()]
        public Task<InitAnnouncementDto> GetInitLabelDto()
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
        public Task<UpdateAnnouncementCommandResult> Update(UpdateAnnouncementCommand cmd)
        {
            var commandResult = 
                _commandBus.DispatchAsync<UpdateAnnouncementCommand, UpdateAnnouncementCommandResult>(cmd);
            return commandResult;
        }

        [HttpGet("{id}")]
        public Task<Nothing> RssNewsChangeState(long id)
        {
               var commandResult =
                _commandBus.DispatchAsync<RssNewsChangeIsProcessCommand, Nothing>(new RssNewsChangeIsProcessCommand(){NewsId = id});
            return commandResult;
        }
    }
}
