using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.Core;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Contract.IntegrationEvents;
using MozaeekCore.Domain.Identity;
using System;
using System.Threading.Tasks;

namespace MozaeekCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMessagePublisher messagePublisher;
        private readonly IUnitOfWork unitOfWork;

        public TestController(IMessagePublisher messagePublisher,
                              IUnitOfWork unitOfWork)
        {
            this.messagePublisher = messagePublisher;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{message}")]
        public async Task Publish(string message)
        {
            var pubevent = new TestEvent(message);
            await messagePublisher.PublishAsync(pubevent);
            await unitOfWork.CommitAsync();
            Console.WriteLine($"{pubevent.Message}:{pubevent.EventId} sent");
        }
        [HttpGet("Call/{message}")]
        public Result<string> Call(string message)
        {
            return ResponseFactory.Create(message + " " + "recieve gateway test");
        }
        [HttpGet("readOperation")]
        [Authorize(Roles = RoleNames.Operation)]
        public IActionResult ReadAgent()
        {
            return Ok("Authorized agent");
        }
        [HttpGet("readOperationAdmin")]
        [Authorize(Roles = RoleNames.Operation + "," + RoleNames.Admin)]
        public IActionResult ReadAgentAdmin()
        {
            return Ok("Authorized agent");
        }
        [HttpGet("readAdmin")]
        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult ReadAdmin()
        {
            return Ok("Authorized admin");
        }

    }



}
