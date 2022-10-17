using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class LabelDeletedConsumer : IConsumer<LabelDeleted>
    {
        private readonly ILabelQueryService _labelQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public LabelDeletedConsumer(ILabelQueryService labelQueryService, IOutboxRepository outboxRepository)
        {
            _labelQueryService = labelQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<LabelDeleted> context)
        {
            await _labelQueryService.Remove(context.Message.Id);

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();

            Console.WriteLine($"Executive LabelDeletedConsumer:{context.Message.Id}:{context.Message.PublishDateTime}");
        }
    }
}