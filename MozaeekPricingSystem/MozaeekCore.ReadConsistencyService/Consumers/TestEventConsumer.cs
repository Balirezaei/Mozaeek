using MassTransit;
using MozaeekCore.Domain.Contract.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class TestEventConsumer :
        IConsumer<TestEvent>
    {
        public Task Consume(ConsumeContext<TestEvent> context)
        {
            Console.WriteLine($"{context.Message.Message}:{context.Message.EventId} recieved");
            return Task.CompletedTask;
        }
    }
}
