using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class RequestTargetDeletedConsumer : IConsumer<RequestTargetDeleted>
    {
        private readonly IRequestTargetQueryService _requestTargetQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestTargetDeletedConsumer(IRequestTargetQueryService requestTargetQueryService, IOutboxRepository outboxRepository)
        {
            _requestTargetQueryService = requestTargetQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<RequestTargetDeleted> context)
        {
            await _requestTargetQueryService.Remove(context.Message.Id);
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
            Console.WriteLine($"Executive RequestTargetDeletedConsumer:{context.Message.Id}:{context.Message.PublishDateTime}");
        }
    }
}