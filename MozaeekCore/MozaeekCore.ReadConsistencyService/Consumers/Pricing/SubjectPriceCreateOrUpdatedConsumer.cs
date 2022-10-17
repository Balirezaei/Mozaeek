using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb.Pricing;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Pricing
{
    public class SubjectPriceCreateOrUpdatedConsumer : IConsumer<SubjectPriceCreateOrUpdated>
    {
        private readonly ISubjectPriceQueryService _subjectPriceQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public SubjectPriceCreateOrUpdatedConsumer(ISubjectPriceQueryService subjectPriceQueryService, IOutboxRepository outboxRepository)
        {
            _subjectPriceQueryService = subjectPriceQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<SubjectPriceCreateOrUpdated> context)
        {
            var @event = context.Message;
            if (@event.IsCreated)
            {
                await _subjectPriceQueryService.Create(new SubjectPriceQuery()
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
                    SubjectPriceDetails = @event.SubjectIds.Select(m => new SubjectPriceDetailQuery { SubjectId = m }).ToList(),
                   
                    PriceCurrencyTitle = @event.PriceCurrencyTitle
                });

                Console.WriteLine($"Executive SubjectPrice Create :{@event.Id},{@event.Title}:{@event.PublishDateTime}");
            }
            else
            {
                await _subjectPriceQueryService.Update(new SubjectPriceQuery()
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
                    SubjectPriceDetails = @event.SubjectIds.Select(m => new SubjectPriceDetailQuery { SubjectId = m }).ToList(),
                    PriceCurrencyTitle = @event.PriceCurrencyTitle
                });
                Console.WriteLine($"Executive SubjectPrice Update:{@event.Id},{@event.Title}:{@event.PublishDateTime}");
            }
            await _outboxRepository.SaveChange();
            Console.WriteLine($"Executive SubjectPrice :{context.Message.Id}:{context.Message.PublishDateTime}");

        }
    }
}