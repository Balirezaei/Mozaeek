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
            if (context.Message.IsCreated)
            {
                await _subjectQueryService.Create(new SubjectQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                Console.WriteLine($"Executive SubjectCreate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
            }
            else
            {
                await _subjectQueryService.Update(new SubjectQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                Console.WriteLine($"Executive SubjectUpdate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
            }

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();



        }
    }
}