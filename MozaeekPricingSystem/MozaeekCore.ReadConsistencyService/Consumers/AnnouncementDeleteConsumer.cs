using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class AnnouncementDeleteConsumer:IConsumer<AnnouncementDeleted>
    {
        private readonly IAnnouncementQueryService _announcementQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public AnnouncementDeleteConsumer(IAnnouncementQueryService announcementQueryService, IOutboxRepository outboxRepository)
        {
            _announcementQueryService = announcementQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<AnnouncementDeleted> context)
        {
            await _announcementQueryService.Remove(context.Message.Id);
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
            Console.WriteLine($"Executive AnnouncementDeleteConsumer :{context.Message.Id}:{context.Message.PublishDateTime}");
        }
    }
}
