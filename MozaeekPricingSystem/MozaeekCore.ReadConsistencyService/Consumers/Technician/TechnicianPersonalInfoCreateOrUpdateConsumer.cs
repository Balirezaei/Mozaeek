using System;
using System.Threading.Tasks;
using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class TechnicianPersonalInfoCreateOrUpdateConsumer : IConsumer<TechnicianPersonalInfoCreatedOrUpdated>
    {
        private readonly ITechnicianQueryService _technicianQueryService;
        private readonly IOutboxRepository _outboxRepository;
        public TechnicianPersonalInfoCreateOrUpdateConsumer(ITechnicianQueryService technicianQueryService, IOutboxRepository outboxRepository)
        {
            _technicianQueryService = technicianQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<TechnicianPersonalInfoCreatedOrUpdated> context)
        {
            try
            {
                var paramater = new TechnicianPersonalInfoParameter()
                {
                    EventId = context.Message.EventId,
                    FirstName = context.Message.FirstName,
                    LastName = context.Message.LastName,
                    IdentityNumber = context.Message.IdentityNumber,
                    TechnicianType = context.Message.TechnicianType,
                    NationalCode = context.Message.NationalCode,
                    PublishEventDate = context.Message.PublishDateTime,
                    TechnicianId = context.Message.TechnicianId,
                    CreateDate = context.Message.CreateDate
                };
                if (context.Message.IsCreated)
                {
                    await _technicianQueryService.CreateTechnicianByPersonalInfo(paramater);
                }
                else
                {
                    await _technicianQueryService.UpdateTechnicianByPersonalInfo(paramater);
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