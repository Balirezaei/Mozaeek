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
            var requestOrg = context.Message;
            if (requestOrg.IsCreated)
            {
                await _requestOrgQueryService.Create(new RequestOrgQuery(requestOrg.Id, requestOrg.Title, requestOrg.Icon, requestOrg.ParentId, requestOrg.PublishDateTime, requestOrg.EventId));
                Console.WriteLine($"Executive RequestOrgCreate:{requestOrg.Id},{requestOrg.Title}:{requestOrg.ParentId}:{requestOrg.PublishDateTime}");
            }
            else
            {
                await _requestOrgQueryService.Update(new RequestOrgQuery(requestOrg.Id, requestOrg.Title,requestOrg.Icon, requestOrg.ParentId, requestOrg.PublishDateTime, requestOrg.EventId));
                Console.WriteLine($"Executive RequestOrgUpdate:{requestOrg.Id},{requestOrg.Title}:{requestOrg.ParentId}:{requestOrg.PublishDateTime}");
            }
            _outboxRepository.UpdateOutboxMesageSatate(requestOrg.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();




        }
    }
}