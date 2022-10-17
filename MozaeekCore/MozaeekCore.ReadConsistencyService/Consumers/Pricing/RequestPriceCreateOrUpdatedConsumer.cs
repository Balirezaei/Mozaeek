using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb.Pricing;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Pricing
{
    public class RequestPriceCreateOrUpdatedConsumer : IConsumer<RequestPriceCreateOrUpdated>
    {
        private readonly IRequestPriceQueryService _requestPriceQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestPriceCreateOrUpdatedConsumer(IRequestPriceQueryService requestPriceQueryService, IOutboxRepository outboxRepository)
        {
            _requestPriceQueryService = requestPriceQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<RequestPriceCreateOrUpdated> context)
        {
            var @event = context.Message;
            if (@event.IsCreated)
            {
                await _requestPriceQueryService.Create(new RequestPriceQuery()
                {
                    Title = @event.Title,
                    SystemShare = @event.SystemShare,
                    PriceCurrencyId = @event.PriceCurrencyId,
                    StartDate = @event.StartDate,
                    PriceAmount = @event.PriceAmount,
                    EndDate = @event.EndDate,
                    IsActive = @event.IsActive,
                    TechnicianShare = @event.TechnicianShare,
                    Id = @event.Id,
                    RequestPriceDetails = @event.RequestIds.Select(m => new RequestPriceDetailQuery { RequestId = m }).ToList(),
                    PriceCurrencyTitle = @event.PriceCurrencyTitle
                });

                Console.WriteLine($"Executive RequestPrice Create :{@event.Id},{@event.Title}:{@event.PublishDateTime}");
            }
            else
            {
                await _requestPriceQueryService.Update(new RequestPriceQuery()
                {
                    Title = @event.Title,
                    SystemShare = @event.SystemShare,
                    PriceCurrencyId = @event.PriceCurrencyId,
                    StartDate = @event.StartDate,
                    PriceAmount = @event.PriceAmount,
                    EndDate = @event.EndDate,
                    IsActive = @event.IsActive,
                    TechnicianShare = @event.TechnicianShare,
                    Id = @event.Id,
                    RequestPriceDetails = @event.RequestIds.Select(m => new RequestPriceDetailQuery { RequestId = m }).ToList(),
                    PriceCurrencyTitle = @event.PriceCurrencyTitle
                });
                Console.WriteLine($"Executive RequestPrice Update:{@event.Id},{@event.Title}:{@event.PublishDateTime}");
            }
            await _outboxRepository.SaveChange();
            Console.WriteLine($"Executive RequestPrice :{context.Message.Id}:{context.Message.PublishDateTime}");

        }
    }

}