using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class RequestDeletedConsumer : IConsumer<RequestDeleted>
    {
        private readonly IRequestQueryService _requestQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestDeletedConsumer(IRequestQueryService requestQueryService, IOutboxRepository outboxRepository)
        {
            _requestQueryService = requestQueryService;
            _outboxRepository = outboxRepository;
        }


        public async Task Consume(ConsumeContext<RequestDeleted> context)
        {
            await _requestQueryService.Remove(context.Message.Id);
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}