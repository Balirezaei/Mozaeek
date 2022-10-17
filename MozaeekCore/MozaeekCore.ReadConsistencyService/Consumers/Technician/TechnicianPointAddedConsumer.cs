using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    //public class TechnicianPointAddedConsumer : IConsumer<TechnicianPointAdded>
    //{
    //    private readonly ITechnicianQueryService _technicianQueryService;
    //    private readonly IOutboxRepository _outboxRepository;

    //    public TechnicianPointAddedConsumer(ITechnicianQueryService technicianQueryService, IOutboxRepository outboxRepository)
    //    {
    //        _technicianQueryService = technicianQueryService;
    //        _outboxRepository = outboxRepository;
    //    }
    //    public async Task Consume(ConsumeContext<TechnicianPointAdded> context)
    //    {
    //        await _technicianQueryService.AddTechnicianPoints(
    //            new TechnicianPointsParameter(context.Message.TechnicianId,
    //                context.Message.PublishDateTime,
    //                context.Message.EventId,
    //                context.Message.Points));

    //        _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
    //        await _outboxRepository.SaveChange();
    //    }
    //}
}