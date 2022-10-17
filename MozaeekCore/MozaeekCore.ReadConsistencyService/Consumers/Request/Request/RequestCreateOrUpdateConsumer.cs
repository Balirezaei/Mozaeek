using MassTransit;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.QueryModel;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace MozaeekCore.ReadConsistencyService.Consumers
{
    public class RequestCreateOrUpdateConsumer : IConsumer<RequestCretedOrUpdated>
    {
        private readonly IRequestQueryService _requestQueryService;
        private readonly IOutboxRepository _outboxRepository;

        public RequestCreateOrUpdateConsumer(IRequestQueryService requestQueryService, IOutboxRepository outboxRepository)
        {
            _requestQueryService = requestQueryService;
            _outboxRepository = outboxRepository;
        }

        public async Task Consume(ConsumeContext<RequestCretedOrUpdated> context)
        {
            var @event = context.Message;
            var parameter = new RequestParameter();
            parameter.EventId = @event.EventId;
            parameter.Id = @event.Id;
            parameter.TargetId = @event.RequestTargetId;
            parameter.ActId = @event.RequestActId;
            //parameter.Documents = @event.RequestDocuments.Select(m => new RequestQueryDependency(m.Description, m.Priority)).ToList();
            parameter.Necessities = @event.RequestNecessities?.Select(m => new RequestQueryDependency(m.Description, m.Priority)).ToList();
            parameter.Actions = @event.RequestActions?.Select(m => new RequestQueryDependency(m.Description, m.Priority)).ToList();
            parameter.QualificationIds = @event.RequestQualifications?.Select(m => new RequestQueryDependency(m.Description, m.Priority)).ToList();
            parameter.PointIds = @event.PointIds;
            parameter.RequestOrgs = @event.RequestOrgs;
            parameter.DefiniteRequestOrgs = @event.DefiniteRequestOrgs;
            parameter.FullOnline = @event.FullOnline;
            parameter.Summary = @event.Description;
            parameter.Regulation = @event.Regulation;
            parameter.RequestExpiredDate = @event.RequestExpiredDate;
            parameter.RequestStartDate = @event.RequestStartDate;

            var conection = @event.RequestTargetConnectionEventDtos
                .Select(m => new ConnectedRequestTarget(m.RequestTargetId, m.Description)).ToList();
            try
            {
                parameter.ConnectedRequestTargets = conection;
                if (@event.IsCreated)
                {
                    await _requestQueryService.Create(parameter);
                    Log.Logger.Information($"Executive RequestCreate:{@event.Id},{@event.EventId}:{@event.PublishDateTime}");
                }
                else
                {
                    await _requestQueryService.Update(parameter);
                    Log.Logger.Information($"Executive RequestUpdate:{@event.Id},{@event.EventId}:{@event.PublishDateTime}");
                }
                _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.Completed);
                await _outboxRepository.SaveChange();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message + " InnerException : " + e.InnerException != null ? e.InnerException.InnerException.Message : "");
                _outboxRepository.UpdateOutboxMesageSatate(context.Message.EventId, OutboxMessageState.ErrorOnConsume);
                await _outboxRepository.SaveChange();
            }
        }
    }
}