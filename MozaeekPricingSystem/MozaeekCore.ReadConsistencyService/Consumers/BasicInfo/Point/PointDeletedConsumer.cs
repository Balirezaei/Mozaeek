using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class PointDeletedConsumer : IConsumer<PointDeleted>
    {
        private readonly IPointQueryService _subjectQueryService;
        private readonly IOutboxRepository _outboxRepository;
        public PointDeletedConsumer(IPointQueryService subjectQueryService,  IOutboxRepository outboxRepository)
        {
            _subjectQueryService = subjectQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<PointDeleted> context)
        {
            
            await _subjectQueryService.Remove(context.Message.Id);
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();

            Console.WriteLine($"Executive PointDeletedConsumer:{context.Message.Id}:{context.Message.PublishDateTime}");
        }
    }
}