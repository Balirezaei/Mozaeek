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
    public class RequestActCreateOrUpdateConsumer : IConsumer<RequestActCretedOrUpdated>
    {
        private readonly IRequestActQueryService _requestActQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestActCreateOrUpdateConsumer(IRequestActQueryService requestActQueryService,  IOutboxRepository outboxRepository)
        {
            _requestActQueryService = requestActQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<RequestActCretedOrUpdated> context)
        {
            if (context.Message.IsCreated)
            {
                await _requestActQueryService.Create(new RequestActQuery(context.Message.Id, context.Message.Title, context.Message.PublishDateTime, context.Message.EventId));
                Console.WriteLine($"Executive RequestActCreate:{context.Message.Id},{context.Message.Title}:{context.Message.PublishDateTime}");
            }
            else
            {
                await _requestActQueryService.Update(new RequestActQuery(context.Message.Id, context.Message.Title, context.Message.PublishDateTime, context.Message.EventId));
                Console.WriteLine($"Executive RequestActUpdate:{context.Message.Id},{context.Message.Title}:{context.Message.PublishDateTime}");
            }

            _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
            await _outboxRepository.SaveChange();




        }
    }
}