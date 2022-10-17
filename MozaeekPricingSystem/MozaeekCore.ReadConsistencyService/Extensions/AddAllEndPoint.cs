using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using MozaeekCore.Messaging.RabbitMQ;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;
using MozaeekCore.ReadConsistencyService.Consumers;
using MozaeekCore.ReadConsistencyService.Consumers.BasicInfo;

namespace MozaeekCore.ReadConsistencyService.Extensions
{
    public static class AllEndPoint
    {
        public static void Set(MassTransitConfig massTransitConfig, ServiceProvider sp, IRabbitMqBusFactoryConfigurator cfg)
        {
            var outboxChangeState = (IOutboxRepository)sp.GetService(typeof(IOutboxRepository));
         
            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":point-create-or-update", e =>
            {
                var service = (IPointQueryService)sp.GetService(typeof(IPointQueryService));
                e.Consumer(() => new PointCreateOrUpdateConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":label-create-or-update", e =>
            {
                var service = (ILabelQueryService)sp.GetService(typeof(ILabelQueryService));
                e.Consumer(() => new LabelCreateOrUpdateConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":requestOrg-create-or-update", e =>
            {
                var service = (IRequestOrgQueryService)sp.GetService(typeof(IRequestOrgQueryService));
                e.Consumer(() => new RequestOrgCreateOrUpdateConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":subject-create-or-update", e =>
            {
                var service = (ISubjectQueryService)sp.GetService(typeof(ISubjectQueryService));
                e.Consumer(() => new SubjectCreateOrUpdateConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":requestAct-create-or-update", e =>
            {
                var service = (IRequestActQueryService)sp.GetService(typeof(IRequestActQueryService));
                e.Consumer(() => new RequestActCreateOrUpdateConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":requestTarget-create-or-update", e =>
            {
                var service = (IRequestTargetQueryService)sp.GetService(typeof(IRequestTargetQueryService));
                e.Consumer(() => new RequestTargetCreateOrUpdateConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":request-create-or-update", e =>
            {
                var service = (IRequestQueryService)sp.GetService(typeof(IRequestQueryService));
                e.Consumer(() => new RequestCreateOrUpdateConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":point-deleted", e =>
            {
                var service = (IPointQueryService)sp.GetService(typeof(IPointQueryService));
                e.Consumer(() => new PointDeletedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":label-deleted", e =>
            {
                var service = (ILabelQueryService)sp.GetService(typeof(ILabelQueryService));
                e.Consumer(() => new LabelDeletedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":requestOrg-deleted", e =>
            {
                var service = (IRequestOrgQueryService)sp.GetService(typeof(IRequestOrgQueryService));
                e.Consumer(() => new RequestOrgDeletedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":subject-deleted", e =>
            {
                var service = (ISubjectQueryService)sp.GetService(typeof(ISubjectQueryService));
                e.Consumer(() => new SubjectDeletedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":requestAct-deleted", e =>
            {
                var service = (IRequestActQueryService)sp.GetService(typeof(IRequestActQueryService));
                e.Consumer(() => new RequestActDeletedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":requestTarget-deleted", e =>
            {
                var service = (IRequestTargetQueryService)sp.GetService(typeof(IRequestTargetQueryService));
                e.Consumer(() => new RequestTargetDeletedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":anouncement-deleted", e =>
            {
                var service = (IAnnouncementQueryService)sp.GetService(typeof(IAnnouncementQueryService));
                e.Consumer(() => new AnnouncementDeleteConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":anouncement-create-or-update", e =>
            {
                var service = (IAnnouncementQueryService)sp.GetService(typeof(IAnnouncementQueryService));
                e.Consumer(() => new AnnouncementCreateOrUpdateConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":technician-contactInfo-create-or-update", e =>
            {
                var service = (ITechnicianQueryService)sp.GetService(typeof(ITechnicianQueryService));
                e.Consumer(() => new TechnicianContactInfoAddedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":technician-educationInfo-create-or-update", e =>
            {
                var service = (ITechnicianQueryService)sp.GetService(typeof(ITechnicianQueryService));
                e.Consumer(() => new TechnicianEducationInfoAddedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":technician-personalInfo-create-or-update", e =>
            {
                var service = (ITechnicianQueryService)sp.GetService(typeof(ITechnicianQueryService));
                e.Consumer(() => new TechnicianPersonalInfoCreateOrUpdateConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":technician-points-create-or-update", e =>
            {
                var service = (ITechnicianQueryService)sp.GetService(typeof(ITechnicianQueryService));
                e.Consumer(() => new TechnicianPointAddedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":technician-requests-create-or-update", e =>
            {
                var service = (ITechnicianQueryService)sp.GetService(typeof(ITechnicianQueryService));
                e.Consumer(() => new TechnicianRequestAddedConsumer(service, outboxChangeState));
            });

            cfg.ReceiveEndpoint(massTransitConfig.EndPoint + ":technician-subjects-create-or-update", e =>
            {
                var service = (ITechnicianQueryService)sp.GetService(typeof(ITechnicianQueryService));
                e.Consumer(() => new TechnicianSubjectAddedConsumer(service, outboxChangeState));
            });
            
        }

    }
}