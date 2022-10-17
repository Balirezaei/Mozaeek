using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb.Pricing;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class SubjectPriceDeletedConsumer : IConsumer<SubjectPriceDeleted>
    {
        private readonly ISubjectPriceQueryService _subjectQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public SubjectPriceDeletedConsumer(ISubjectPriceQueryService subjectQueryService, IOutboxRepository outboxRepository)
        {
            _subjectQueryService = subjectQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<SubjectPriceDeleted> context)
        {
            await _subjectQueryService.Remove(context.Message.Id);

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();

            Console.WriteLine($"Executive SubjectPriceDeletedConsumer:{context.Message.Id}:{context.Message.PublishDateTime}");
        }
    }
}