using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class SubjectCreateOrUpdateConsumer : IConsumer<SubjectCretedOrUpdated>
    {
        private readonly ISubjectQueryService _subjectQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public SubjectCreateOrUpdateConsumer(ISubjectQueryService subjectQueryService, IOutboxRepository outboxRepository)
        {
            _subjectQueryService = subjectQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<SubjectCretedOrUpdated> context)
        {
            var subject = context.Message;
            if (subject.IsCreated)
            {
                await _subjectQueryService.Create(new SubjectQuery(subject.Id, subject.Title, subject.Icon, subject.ParentId, subject.PublishDateTime, subject.EventId));
                Console.WriteLine($"Executive SubjectCreate:{subject.Id},{subject.Title}:{subject.ParentId}:{subject.PublishDateTime}");
            }
            else
            {
                await _subjectQueryService.Update(new SubjectQuery(subject.Id, subject.Title, subject.Icon, subject.ParentId, subject.PublishDateTime, subject.EventId));
                Console.WriteLine($"Executive SubjectUpdate:{subject.Id},{subject.Title}:{subject.ParentId}:{subject.PublishDateTime}");
            }

            _outboxRepository.UpdateOutboxMesageSatate(subject.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();



        }
    }
}