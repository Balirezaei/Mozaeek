using System.Threading.Tasks;
using MassTransit;
using Mozaeek.CR.PublicDto;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Enum;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;
using MozaeekCore.SubDomainEvent;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    //public class TechnicianContactInfoAddedConsumer : IConsumer<TechnicianContactInfoAdded>
    //{
    //    private readonly ITechnicianQueryService _technicianQueryService;
    //    private readonly IOutboxRepository _outboxRepository;
    //    private readonly IMessagePublisher _messagePublisher;
    //    public TechnicianContactInfoAddedConsumer(ITechnicianQueryService technicianQueryService, IOutboxRepository outboxRepository, IMessagePublisher messagePublisher)
    //    {
    //        _technicianQueryService = technicianQueryService;
    //        _outboxRepository = outboxRepository;
    //        _messagePublisher = messagePublisher;
    //    }
    //    public async Task Consume(ConsumeContext<TechnicianContactInfoAdded> context)
    //    {
    //        await _technicianQueryService.AddTechnicianContactInfo(
    //            new TechnicianContactInfoParameter(context.Message.TechnicianId,
    //                context.Message.EventId,
    //                context.Message.PublishDateTime,
    //                context.Message.MobileNumber,
    //                context.Message.PhoneNumber,
    //                context.Message.OfficeNumber,
    //                context.Message.PostalCode,
    //                context.Message.Address));
            
    //        var technician = await _technicianQueryService.Get(context.Message.TechnicianId);
    //        await _messagePublisher.PublishAsync(new TechnicianRegistered(technician.Id, technician.FirstName, technician.LastName,
    //            technician.NationalCode, technician.ContactInfoMobileNumber, technician.TechnicianType == TechnicianType.Guide));
    //        _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            
    //        await _outboxRepository.SaveChange();

    //    }
    //}
}