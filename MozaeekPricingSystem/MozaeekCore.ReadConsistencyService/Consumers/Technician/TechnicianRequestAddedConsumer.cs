using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class TechnicianRequestAddedConsumer : IConsumer<TechnicianRequestAdded>
    {
        private readonly ITechnicianQueryService _technicianQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public TechnicianRequestAddedConsumer(ITechnicianQueryService technicianQueryService, IOutboxRepository outboxRepository)
        {
            _technicianQueryService = technicianQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<TechnicianRequestAdded> context)
        {
            await _technicianQueryService.AddTechnicianRequests(
                new TechnicianRequestsParameter(context.Message.TechnicianId,
                    context.Message.PublishDateTime,
                    context.Message.EventId,
                    context.Message.Requests));

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}