using System;
using System.Threading.Tasks;
using MozaeekUserProfile.Core.Core.Events;
using MozaeekUserProfile.Core.Core.MessagePublisher;

namespace MozaeekUserProfile.OutBoxManagement
{
    public class OutboxPublisher : IMessagePublisher
    {
        private readonly IOutboxRepository _repository;

        public OutboxPublisher(IOutboxRepository repository)
        {
            this._repository = repository;
        }

        public async Task PublishAsync(object @event)
        {
            Guid eventId = (Guid) @event.GetType().GetProperty("EventId").GetValue(@event, null);
            DateTime publishDateTime =
                (DateTime) @event.GetType().GetProperty("PublishDateTime").GetValue(@event, null);

            var outboxMessage = new OutboxMessage(@event, eventId, publishDateTime);
            _repository.CreateOutboxMessage(outboxMessage);
            await _repository.SaveChange();

        }

    }

}