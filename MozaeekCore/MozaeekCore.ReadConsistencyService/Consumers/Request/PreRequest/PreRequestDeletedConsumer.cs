using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class PreRequestDeletedConsumer : IConsumer<PreRequestDeleted>
    {
        private readonly IPreRequestQueryService _queryService;
        private readonly IOutboxRepository _outboxRepository;

        public PreRequestDeletedConsumer(IPreRequestQueryService queryService, IOutboxRepository outboxRepository)
        {
            _queryService = queryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<PreRequestDeleted> context)
        {
            await _queryService.Remove(context.Message.Id);
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}