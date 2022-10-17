using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class PreRequestCreatedConsumer : IConsumer<PreRequestCreated>
    {
        private readonly IPreRequestQueryService _queryService;
        private readonly IOutboxRepository _outboxRepository;

        public PreRequestCreatedConsumer(IPreRequestQueryService queryService, IOutboxRepository outboxRepository)
        {
            _queryService = queryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<PreRequestCreated> context)
        {
            await _queryService.Create(new PreRequestQuery()
            {
                Title = context.Message.Title,
                Summery = context.Message.Summery,
                IsProcessed = context.Message.IsProcessed,
                CreateDateTime = context.Message.CreateDateTime,
                LastEventId = context.Message.EventId,
                LastEventPublishDate = context.Message.PublishDateTime,
                Id = context.Message.Id
            });
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}