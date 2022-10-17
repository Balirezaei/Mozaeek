using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class AnnouncementAssignRequestEventConsumer : IConsumer<AnnouncementAssignRequestEvent>
    {
        private readonly IAnnouncementQueryService _announcementQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public AnnouncementAssignRequestEventConsumer(IAnnouncementQueryService announcementQueryService, IOutboxRepository outboxRepository)
        {
            _announcementQueryService = announcementQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<AnnouncementAssignRequestEvent> context)
        {
            var @event = context.Message;
            await _announcementQueryService.AssignRequest(@event.AnnouncementId, @event.RequestId);
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}