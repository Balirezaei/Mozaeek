using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.BasicInfo;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers.BasicInfo
{
    public class PointCreateOrUpdateConsumer : IConsumer<PointCreatedOrUpdated>
    {
        private readonly IPointQueryService _pointQueryService;
        private readonly IOutboxRepository _outboxRepository;
        public PointCreateOrUpdateConsumer(IPointQueryService pointQueryService, IOutboxRepository outboxRepository)
        {
            _pointQueryService = pointQueryService;
            _outboxRepository = outboxRepository;
        }
        public async Task Consume(ConsumeContext<PointCreatedOrUpdated> context)
        {
            if (context.Message.IsCreated)
            {
                await _pointQueryService.Create(new PointQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                Console.WriteLine($"Executive PointCreate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
            }
            else
            {
                await _pointQueryService.Update(new PointQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                Console.WriteLine($"Executive PointUpdate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
            }
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}