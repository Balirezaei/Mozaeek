using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class DefiniteRequestOrgDeletedConsumer : IConsumer<DefiniteRequestOrgDeleted>
    {
        private readonly IDefiniteRequestOrgQueryService _definiteRequestOrgQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public DefiniteRequestOrgDeletedConsumer(IDefiniteRequestOrgQueryService definiteRequestOrgQueryService, IOutboxRepository outboxRepository)
        {
            _definiteRequestOrgQueryService = definiteRequestOrgQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<DefiniteRequestOrgDeleted> context)
        {
            await _definiteRequestOrgQueryService.Remove(context.Message.Id);

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();

            Console.WriteLine($"Executive DefiniteRequestOrgDeleted:{context.Message.Id}:{context.Message.PublishDateTime}");

        }
    }
}