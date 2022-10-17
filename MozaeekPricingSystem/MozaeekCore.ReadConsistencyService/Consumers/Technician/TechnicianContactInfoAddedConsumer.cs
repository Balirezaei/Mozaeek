using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class TechnicianContactInfoAddedConsumer : IConsumer<TechnicianContactInfoAdded>
    {
        private readonly ITechnicianQueryService _technicianQueryService;
        private readonly IOutboxRepository _outboxRepository;
        public TechnicianContactInfoAddedConsumer(ITechnicianQueryService technicianQueryService, IOutboxRepository outboxRepository)
        {
            _technicianQueryService = technicianQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<TechnicianContactInfoAdded> context)
        {
            await _technicianQueryService.AddTechnicianContactInfo(
                new TechnicianContactInfoParameter(context.Message.TechnicianId,
                    context.Message.EventId,
                    context.Message.PublishDateTime,
                    context.Message.MobileNumber,
                    context.Message.PhoneNumber,
                    context.Message.OfficeNumber,
                    context.Message.PostalCode,
                    context.Message.Address));

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}