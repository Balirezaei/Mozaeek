using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MozaeekCore.Core;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Core.ResponseMessages;
using MozaeekCore.Domain.Contract.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return ResponseFactory.Create( message + " " + "recieve gateway");
        }
    }
}
