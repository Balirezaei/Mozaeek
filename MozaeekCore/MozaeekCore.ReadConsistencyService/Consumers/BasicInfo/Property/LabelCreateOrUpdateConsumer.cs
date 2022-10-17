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
    public class PropertyCreateOrUpdateConsumer : IConsumer<PropertyCreatedOrUpdated>
    {
        private readonly IPropertyQueryService _PropertyQueryService;
        private readonly IOutboxRepository _outboxRepository;


        public PropertyCreateOrUpdateConsumer(IPropertyQueryService PropertyQueryService, IOutboxRepository outboxRepository)
        {
            _PropertyQueryService = PropertyQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<PropertyCreatedOrUpdated> context)
        {
            try
            {
                if (context.Message.IsCreated)
                {
                    await _PropertyQueryService.Create(new PropertyQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                    Console.WriteLine($"Executive PropertyCreate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
                }
                else
                {
                    await _PropertyQueryService.Update(new PropertyQuery(context.Message.Id, context.Message.Title, context.Message.ParentId, context.Message.PublishDateTime, context.Message.EventId));
                    Console.WriteLine($"Executive PropertyUpdate:{context.Message.Id},{context.Message.Title}:{context.Message.ParentId}:{context.Message.PublishDateTime}");
                }
                _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
                await _outboxRepository.SaveChange();
            }
            catch 
            {
                
            }
          

        }
    }
}