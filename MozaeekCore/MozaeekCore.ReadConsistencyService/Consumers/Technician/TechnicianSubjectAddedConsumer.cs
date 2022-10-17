using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    //public class TechnicianSubjectAddedConsumer : IConsumer<TechnicianSubjectAdded>
    //{
    //    private readonly ITechnicianQueryService _technicianQueryService;
    //    private readonly IOutboxRepository _outboxRepository;

    //    public TechnicianSubjectAddedConsumer(ITechnicianQueryService technicianQueryService, IOutboxRepository outboxRepository)
    //    {
    //        _technicianQueryService = technicianQueryService;
    //        _outboxRepository = outboxRepository;
    //    }
    //    public async Task Consume(ConsumeContext<TechnicianSubjectAdded> context)
    //    {
    //        await _technicianQueryService.AddTechnicianSubjects(
    //            new TechnicianSubjectsParameter(context.Message.TechnicianId,
    //                context.Message.PublishDateTime,
    //                context.Message.EventId,
    //                context.Message.Subjects));

    //        _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
    //        await _outboxRepository.SaveChange();
    //    }
    //}
}