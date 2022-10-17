using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class RequestTargetCreateOrUpdateConsumer : IConsumer<RequestTargetCreatedOrUpdated>
    {
        private readonly IRequestTargetQueryService _requestTargetQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestTargetCreateOrUpdateConsumer(IRequestTargetQueryService requestTargetQueryService, IOutboxRepository outboxRepository)
        {
            _requestTargetQueryService = requestTargetQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<RequestTargetCreatedOrUpdated> context)
        {
            var @event = context.Message;
            var parameter = new RequestTargetParameter(@event.Id, @event.Title, @event.Icon, @event.SubjectIds, @event.LabelIds,
              @event.IsDocument, @event.EventId, @event.PublishDateTime);

            if (@event.IsCreated)
            {
                await _requestTargetQueryService.Create(parameter);
                Console.WriteLine($"Executive RequestTargetCreate:{@event.Id},{@event.Title}:{@event.PublishDateTime}");
            }
            else
            {
                await _requestTargetQueryService.Update(parameter);
                Console.WriteLine($"Executive RequestTargetUpdate:{@event.Id},{@event.Title}:{@event.PublishDateTime}");
            }

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();



        }
    }
}