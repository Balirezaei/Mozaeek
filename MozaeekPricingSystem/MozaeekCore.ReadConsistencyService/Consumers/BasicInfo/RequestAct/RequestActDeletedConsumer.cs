using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class RequestActDeletedConsumer : IConsumer<RequestActDeleted>
    {
        private readonly IRequestActQueryService _labelActQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestActDeletedConsumer(IRequestActQueryService labelActQueryService, IOutboxRepository outboxRepository)
        {
            _labelActQueryService = labelActQueryService;
            _outboxRepository = outboxRepository;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<RequestActDeleted> context)
        {
            await _labelActQueryService.Remove(context.Message.Id);
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();

            Console.WriteLine($"Executive RequestActDeletedConsumer:{context.Message.Id}:{context.Message.PublishDateTime}");
        }
    }
}