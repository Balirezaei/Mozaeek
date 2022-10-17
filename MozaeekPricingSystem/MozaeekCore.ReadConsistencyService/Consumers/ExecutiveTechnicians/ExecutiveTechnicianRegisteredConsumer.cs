using MassTransit;
using MozaeekCore.Domain.Contract.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MozaeekCore.ReadConsistencyService.Consumers.ExecutiveTechnicians
{
    public class ExecutiveTechnicianRegisteredConsumer :
        IConsumer<ExecutiveTechnicianRegistered>
    {
        public Task Consume(ConsumeContext<ExecutiveTechnicianRegistered> context)
        {
            //ToDo: Write into the read database
            Console.WriteLine($"Executive Tech:{context.Message.FirstName},{context.Message.LastName}:{context.Message.NationalCode}:{context.Message.CreateDateTime}");
            return Task.CompletedTask;
        }
    }
}
