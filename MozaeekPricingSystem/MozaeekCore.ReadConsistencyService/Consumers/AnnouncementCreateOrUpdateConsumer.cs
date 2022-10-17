using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class AnnouncementCreateOrUpdateConsumer:IConsumer<AnnouncementCreatedOrUpdated>
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
            var @event = context.Message;
            var parameter = new AnnouncementParameter
            {
                Description = @event.Description,
                RequestTargetId = @event.RequestTargetId,
                Id = @event.Id,
                Title = @event.Title,
                EventId = @event.EventId,
                PointIds = @event.PointIds,
                PublishEventDate = @event.PublishDateTime
            };

            if (@event.IsCreated)
            {
                await _requestTargetQueryService.Create(parameter);
                Console.WriteLine($"Executive AnnouncementCreate:{@event.Id},{@event.Title}:{@event.PublishDateTime}");
            }
            else
            {
                await _requestTargetQueryService.Update(parameter);
                Console.WriteLine($"Executive AnnouncementUpdate:{@event.Id},{@event.Title}:{@event.PublishDateTime}");
            }

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();



        }
    }
}