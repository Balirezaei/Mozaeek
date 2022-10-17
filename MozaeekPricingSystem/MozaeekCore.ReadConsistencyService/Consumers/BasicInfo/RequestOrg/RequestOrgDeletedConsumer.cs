using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class RequestOrgDeletedConsumer : IConsumer<RequestOrgDeleted>
    {
        private readonly IRequestOrgQueryService _requestOrgQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestOrgDeletedConsumer(IRequestOrgQueryService requestOrgQueryService, IOutboxRepository outboxRepository)
        {
            _requestOrgQueryService = requestOrgQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<RequestOrgDeleted> context)
        {
            await _requestOrgQueryService.Remove(context.Message.Id);
           
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();

            Console.WriteLine($"Executive RequestOrgDeletedConsumer:{context.Message.Id}:{context.Message.PublishDateTime}");
        }
    }
}