using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class PreRequestUpdatedConsumer : IConsumer<PreRequestUpdated>
    {
        private readonly IPreRequestQueryService _queryService;
        private readonly IOutboxRepository _outboxRepository;

        public PreRequestUpdatedConsumer(IPreRequestQueryService queryService, IOutboxRepository outboxRepository)
        {
            _queryService = queryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<PreRequestUpdated> context)
        {
            await _queryService.Update(new PreRequestQuery()
            {
                Title = context.Message.Title,
                Summery = context.Message.Summery,
                IsProcessed = context.Message.IsProcessed,
                LastEventId = context.Message.EventId,
                LastEventPublishDate = context.Message.PublishDateTime,
                Id = context.Message.Id
            });
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}