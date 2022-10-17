using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Domain.Contract.Technician;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;
using Newtonsoft.Json;
using System.Linq;
namespace MozaeekCore.ReadConsistencyService.Consumers.Technician
{
    public class TechnicianCreateOrUpdateConsumer : IConsumer<TechnicianCreatedOrUpdated>
    {
        private readonly ITechnicianQueryService _technicianQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public TechnicianCreateOrUpdateConsumer(ITechnicianQueryService technicianQueryService, IOutboxRepository outboxRepository)
        {
            _technicianQueryService = technicianQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<TechnicianCreatedOrUpdated> context)
        {
            try
            {
                var @event = context.Message;
                var parameter = new TechnicianParameter
                {
                    SecondVerification = @event.SecondVerification,
                    FirstName = @event.FirstName,
                    SubjectIds = @event.SubjectIds,
                    CreateDateTime = @event.CreateDateTime,
                    DefiniteRequestOrgIds = @event.DefiniteRequestOrgIds,
                    Address = @event.Address,
                    // Attachments=@event.Attachments,
                    Email = @event.Email,
                    FirstVerification = @event.FirstVerification,
                    Id = @event.Id,
                    LastName = @event.LastName,
                    NationalId = @event.NationalId,
                    TechnicianType = @event.TechnicianType,
                    PhoneNumber = @event.PhoneNumber,
                    OfflineRequestTargetIds = @event.OfflineRequestTargetIds,
                    PointId = @event.PointId,
                    PostalCode = @event.PostalCode,
                    Attachments=@event.Attachments.Select(a=>new TechnicianAttachmentQuery() { FileId=a.FileId,FileHttpAddress=a.FileHttpAddress,FileName=a.FileName}).ToList()
                };
                Console.WriteLine($"@eventId {@event.Id}");
                Console.WriteLine($"@event.SerializeObject {JsonConvert.SerializeObject(context.Message)}");

                if (@event.IsCreated)
                {
                    await _technicianQueryService.CreateTechnician(parameter);
                    Console.WriteLine($"Executive TechnicianCreate:{@event.Id},{@event.FirstName}:{@event.CreateDateTime}");
                }
                else
                {
                    //  await _technicianQueryService.Update(parameter);
                    Console.WriteLine($"Executive AnnouncementUpdate:{@event.Id},{@event.FirstName}:{@event.CreateDateTime}");
                }

                _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
                await _outboxRepository.SaveChange();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (e.InnerException != null)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }
        }
    }
}
