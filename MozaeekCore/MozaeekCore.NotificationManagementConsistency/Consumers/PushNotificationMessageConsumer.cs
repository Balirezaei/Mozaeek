using System.Threading.Tasks;
using MassTransit;
using Mozaeek.CR.PublicDto.Dto;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.OutBoxManagement;

namespace MozaeekCore.NotificationManagementConsistency.Consumers
{
    public class PushNotificationMessageConsumer : IConsumer<PushNotificationMessage>
    {
        private readonly PushNotificationService _pushNotificationService;
        private readonly IOutboxRepository _outboxRepository;
        public PushNotificationMessageConsumer(PushNotificationService pushNotificationService, IOutboxRepository outboxRepository)
        {
            _pushNotificationService = pushNotificationService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<PushNotificationMessage> context)
        {
           await _pushNotificationService.SendNotif(context.Message);
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId,OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}