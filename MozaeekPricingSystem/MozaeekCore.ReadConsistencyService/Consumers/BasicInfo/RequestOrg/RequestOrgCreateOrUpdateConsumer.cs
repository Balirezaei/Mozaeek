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
    public class RequestOrgCreateOrUpdateConsumer : IConsumer<RequestOrgCretedOrUpdated>
    {
        private readonly IRequestOrgQueryService _requestOrgQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestOrgCreateOrUpdateConsumer(IRequestOrgQueryService requestOrgQueryService, IOutboxRepository outboxRepository)
        {
            _requestOrgQueryService = requestOrgQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<RequestOrgCretedOrUpdated> context)
        {
            if (context.Message.IsCreated)
            {
                await _requestOrgQueryService.Create(new RequestOrgQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                Console.WriteLine($"Executive RequestOrgCreate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
            }
            else
            {
                await _requestOrgQueryService.Update(new RequestOrgQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                Console.WriteLine($"Executive RequestOrgUpdate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
            }
            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();




        }
    }
}