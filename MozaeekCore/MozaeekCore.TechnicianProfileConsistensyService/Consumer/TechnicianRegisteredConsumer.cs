using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.TechnicianProfileConsistensyService.Model;
using MozaeekCore.TechnicianProfileConsistensyService.Service;

namespace MozaeekCore.TechnicianProfileConsistensyService.Consumer
{
    // public class TechnicianRegisteredConsumer : IConsumer<TechnicianRegistered>
    // {
    //     private ITechnicianSynchronizeService _technicianSynchronizeService;
    //     private IOutboxRepository _outboxRepository;
    //
    //     public TechnicianRegisteredConsumer(ITechnicianSynchronizeService technicianSynchronizeService, IOutboxRepository outboxRepository)
    //     {
    //         _technicianSynchronizeService = technicianSynchronizeService;
    //         _outboxRepository = outboxRepository;
    //     }
    //     
    //     public async Task Consume(ConsumeContext<TechnicianRegistered> context)
    //     {
    //         try
    //         {
    //             var tech = context.Message;
    //             await _technicianSynchronizeService.SaveIfNotExist(new Tecnician(tech.TechnicianId, tech.FirstName,
    //                 tech.LastName, tech.MobileNumber, tech.NationalCode, tech.IsConsultant));
    //
    //             _outboxRepository.UpdateOutboxMesageSatate(tech.EventId, OutboxMessageState.Completed);
    //             await _outboxRepository.SaveChange();
    //         }
    //         catch (Exception e)
    //         {
    //
    //         }
    //     }
    // }
}