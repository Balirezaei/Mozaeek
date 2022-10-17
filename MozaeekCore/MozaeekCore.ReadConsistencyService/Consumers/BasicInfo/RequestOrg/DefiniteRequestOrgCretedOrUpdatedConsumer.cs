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
    public class DefiniteRequestOrgCretedOrUpdatedConsumer : IConsumer<DefiniteRequestOrgCretedOrUpdated>
    {
        private readonly IDefiniteRequestOrgQueryService _DefiniteRequestOrgQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public DefiniteRequestOrgCretedOrUpdatedConsumer(IDefiniteRequestOrgQueryService DefiniteRequestOrgQueryService, IOutboxRepository outboxRepository)
        {
            _DefiniteRequestOrgQueryService = DefiniteRequestOrgQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<DefiniteRequestOrgCretedOrUpdated> context)
        {
            var @event = context.Message;
            if (@event.IsCreated)
            {
                await _DefiniteRequestOrgQueryService.Create(new DefiniteRequestOrgQueryParameter()
                {
                    LastEventPublishDate = @event.PublishDateTime,
                    Address = @event.Address,
                    PhoneNumber = @event.PhoneNumber,
                    LastEventId = @event.EventId,
                    Point = @event.PointId,
                    RequestOrg = @event.RequestOrgId,
                    Id = @event.Id
                });
            }
            else
            {
                await _DefiniteRequestOrgQueryService.Update(new DefiniteRequestOrgQueryParameter()
                {
                    LastEventPublishDate = @event.PublishDateTime,
                    Address = @event.Address,
                    PhoneNumber = @event.PhoneNumber,
                    LastEventId = @event.EventId,
                    Point = @event.PointId,
                    RequestOrg = @event.RequestOrgId,
                    Id = @event.Id
                });
            }
            Console.WriteLine($"Executive DefiniteRequestOrg:{context.Message.Id}:{context.Message.PublishDateTime}");
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();
        }
    }
}