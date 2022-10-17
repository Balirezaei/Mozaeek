using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class SubjectDeletedConsumer : IConsumer<SubjectDeleted>
    {
        private readonly ISubjectQueryService _subjectQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public SubjectDeletedConsumer(ISubjectQueryService subjectQueryService, IOutboxRepository outboxRepository)
        {
            _subjectQueryService = subjectQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<SubjectDeleted> context)
        {
            await _subjectQueryService.Remove(context.Message.Id);
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
            Console.WriteLine($"Executive SubjectDeletedConsumer:{context.Message.Id}:{context.Message.PublishDateTime}");
        }
    }
}