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
    public class LabelCreateOrUpdateConsumer : IConsumer<LabelCreatedOrUpdated>
    {
        private readonly ILabelQueryService _labelQueryService;
        private readonly IOutboxRepository _outboxRepository;


        public LabelCreateOrUpdateConsumer(ILabelQueryService labelQueryService, IOutboxRepository outboxRepository)
        {
            _labelQueryService = labelQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<LabelCreatedOrUpdated> context)
        {
            try
            {
                if (context.Message.IsCreated)
                {
                    await _labelQueryService.Create(new LabelQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                    Console.WriteLine($"Executive LabelCreate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
                }
                else
                {
                    await _labelQueryService.Update(new LabelQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                    Console.WriteLine($"Executive LabelUpdate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
                }
                _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
                await _outboxRepository.SaveChange();
            }
            catch (Exception e)
            {
                
            }
          

        }
    }
}