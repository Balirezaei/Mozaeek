using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class RequestCreateOrUpdateConsumer : IConsumer<RequestCretedOrUpdated>
    {
        private readonly IRequestQueryService _requestQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestCreateOrUpdateConsumer(IRequestQueryService requestQueryService, IOutboxRepository outboxRepository)
        {
            _requestQueryService = requestQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<RequestCretedOrUpdated> context)
        {
            var @event = context.Message;
            var parameter = new RequestParameter(@event.Id, @event.RequestTargetId, @event.RequestActId,
                @event.RequestDocuments.Select(m => new RequestQueryDependency(m.Description, m.Priority)).ToList()
, @event.RequestNessesities.Select(m => new RequestQueryDependency(m.Description, m.Priority)).ToList()
, @event.RequestActions.Select(m => new RequestQueryDependency(m.Description, m.Priority)).ToList()
, @event.RequestQualifications.Select(m => new RequestQueryDependency(m.Description, m.Priority)).ToList()
                , @event.PointIds);

            if (@event.IsCreated)
            {
                await _requestQueryService.Create(parameter);
                Console.WriteLine($"Executive RequestCreate:{@event.Id},{@event.EventId}:{@event.PublishDateTime}");
            }
            else
            {
                await _requestQueryService.Update(parameter);
                Console.WriteLine($"Executive RequestUpdate:{@event.Id},{@event.EventId}:{@event.PublishDateTime}");
            }
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();



        }
    }
}