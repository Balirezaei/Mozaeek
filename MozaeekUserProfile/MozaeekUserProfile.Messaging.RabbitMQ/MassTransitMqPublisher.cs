using MassTransit;
using MozaeekUserProfile.Core.MessagePublisher;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekUserProfile.Messaging.RabbitMQ
{
    public class MassTransitMqPublisher : IMessagePublisher
    {
        private readonly IPublishEndpoint publishEndpoint;

        public MassTransitMqPublisher(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }
        public Task PublishAsync(object @event)
        {
            return publishEndpoint.Publish(@event);
        }
    }
}
