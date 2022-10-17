using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    //public class TechnicianEducationInfoAddedConsumer : IConsumer<TechnicianEducationInfoAdded>
    //{
    //    private readonly ITechnicianQueryService _technicianQueryService;
    //    private readonly IOutboxRepository _outboxRepository;

    //    public TechnicianEducationInfoAddedConsumer(ITechnicianQueryService technicianQueryService, IOutboxRepository outboxRepository)
    //    {
    //        _technicianQueryService = technicianQueryService;
    //        _outboxRepository = outboxRepository;
    //    }

    //    public async Task Consume(ConsumeContext<TechnicianEducationInfoAdded> context)
    //    {
    //        await _technicianQueryService.AddTechnicianEducaionInfo(
    //            new TechnicianEducationInfoParameter(context.Message.TechnicianId,
    //                context.Message.EventId,
    //                context.Message.PublishDateTime,
    //                context.Message.EducationGradeId,
    //                context.Message.EducationGradeTitle,
    //                context.Message.EducationFieldId,
    //                context.Message.EducationFieldTitle));

    //        _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
    //        await _outboxRepository.SaveChange();
    //    }
    //}
}