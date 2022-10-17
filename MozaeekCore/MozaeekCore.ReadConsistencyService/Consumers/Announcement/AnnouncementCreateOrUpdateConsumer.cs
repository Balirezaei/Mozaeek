using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;
using Newtonsoft.Json;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class AnnouncementCreateOrUpdateConsumer : IConsumer<AnnouncementCreatedOrUpdated>
    {
        private readonly IAnnouncementQueryService _requestTargetQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public AnnouncementCreateOrUpdateConsumer(IAnnouncementQueryService requestTargetQueryService, IOutboxRepository outboxRepository)
        {
            _requestTargetQueryService = requestTargetQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<AnnouncementCreatedOrUpdated> context)
        {
            try
            {
                var @event = context.Message;
                var parameter = new AnnouncementParameter
                {
                    Description = @event.Description,
                    LabelIds = @event.LabelIds,
                    RequestOrgIds = @event.RequestOrgIds,
                    SubjectIds = @event.SubjectIds,
                    Id = @event.Id,
                    Title = @event.Title,
                    EventId = @event.EventId,
                    PointIds = @event.PointIds,
                    PublishEventDate = @event.PublishDateTime,
                    ImageUrl = @event.ImageUrl,
                    CreateDate = @event.ReleaseDate,
                    Summary = @event.Summary,
                    HasRequest = @event.HasRequest,
                };
                Console.WriteLine($"@event.RequestId {@event.RequestId}");
                Console.WriteLine($"@event.HasRequest {@event.HasRequest}");
                Console.WriteLine($"@event.SerializeObject {JsonConvert.SerializeObject(context.Message)}");

                if (@event.IsCreated)
                {
                    await _requestTargetQueryService.Create(parameter);
                    Console.WriteLine($"Executive AnnouncementCreate:{@event.Id},{@event.Title}:{@event.ReleaseDate}");
                }
                else
                {
                    await _requestTargetQueryService.Update(parameter);
                    Console.WriteLine($"Executive AnnouncementUpdate:{@event.Id},{@event.Title}:{@event.ReleaseDate}");
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