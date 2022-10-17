using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Messaging.RabbitMQ
{
    public class MassTransitMqPublisher : IMessagePublisher
    {
        private readonly IPublishEndpoint publishEndpoint;

        public MassTransitMqPublisher(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public PublisherType Key { get => PublisherType.Queue; }

        public Task PublishAsync(object @event)
        {
            return publishEndpoint.Publish(@event);
        }
    }
}
