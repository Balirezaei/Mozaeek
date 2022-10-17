using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb.Pricing;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class RequestPriceDeletedConsumer : IConsumer<RequestPriceDeleted>
    {
        private readonly IRequestPriceQueryService _requestPriceQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestPriceDeletedConsumer(IRequestPriceQueryService requestQueryService, IOutboxRepository outboxRepository)
        {
            _requestPriceQueryService = requestQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<RequestPriceDeleted> context)
        {
            await _requestPriceQueryService.Remove(context.Message.Id);

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();

            Console.WriteLine($"Executive RequestPriceDeletedConsumer:{context.Message.Id}:{context.Message.PublishDateTime}");
        }
    }
}