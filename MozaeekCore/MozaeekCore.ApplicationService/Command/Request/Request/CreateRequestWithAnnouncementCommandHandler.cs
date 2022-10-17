using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.Mapper;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRequestWithAnnouncementCommandHandler : IBaseAsyncCommandHandler<CreateRequestWithAnnouncementCommand, CreateRequestCommandResult>
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IAnnouncementQueryService _announcementQuery;

        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;
        public CreateRequestWithAnnouncementCommandHandler(IRequestRepository requestRepository, IUnitOfWork unitOfWork, IMessagePublisher publisher, IAnnouncementRepository announcementRepository, IAnnouncementQueryService announcementQuery)
        {
            _requestRepository = requestRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
            _announcementRepository = announcementRepository;
            _announcementQuery = announcementQuery;
        }

        public async Task<CreateRequestCommandResult> HandleAsync(CreateRequestWithAnnouncementCommand cmd)
        {
            var requestPoints = cmd.Points.Select(m => m.Id).Select(m => new RequestPoint()
            {
                PointId = m
            }).ToList();

            var requestActions = cmd.RequestActions.Select(m => new RequestAction(0, m.Description, m.Priority))
                .ToList();

            var requestNecessities = cmd.RequestNecessities
                .Select(m => new RequestNecessity(0, m.Description, m.Priority)).ToList();
            RequestDomainInput input;
            if (cmd.FullOnline)
            {
                var requestOrgs = cmd.RequestOrgs
                    .Select(m => new RequestRequestOrg()
                    {
                        RequestOrgId = m.Id
                    }).ToList();
                input = new OnlineRequestInput(requestOrgs);
            }
            else
            {
                var definiterequestOrgs = cmd.DefiniteRequestOrgDtos
                    .Select(m => new RequestDefiniteRequestOrg()
                    {
                        DefiniteRequestOrgId = m.Id
                    }).ToList();
                input = new OfflineRequestInput(definiterequestOrgs);
            }

            var requestQualifications = cmd.RequestQualifications
                .Select(m => new RequestQualification(0, m.Description, m.Priority)).ToList();


            var requestConnection = cmd.ConnectionDtos
                .Select(m => new RequestTargetConnection(0, m.RequestTargetId, m.Description)).ToList();

            var requestDependency = new RequestDependencyInput(requestActions, requestNecessities, requestQualifications);

            var request = new Request(cmd.RequestTargetId, cmd.RequestActId, input,
                requestDependency,
                requestPoints,
                requestConnection,
                cmd.RequestExpiredDate,
                cmd.RequestStartDate,
                cmd.Summary,
                cmd.Regulation);

            _requestRepository.Add(request);
            //var announcement = await _announcementRepository.Find(cmd.AnnouncementId);
            //announcement.AssignRequest(request);

            //await _announcementQuery.AssignRequest(cmd.AnnouncementId, request.Id);

            await _unitOfWork.CommitAsync();

            var @event = new RequestCretedOrUpdated(
                request.Id
                , cmd.RequestTargetId,
                cmd.RequestActId,
                requestActions.Select(m => m.GetRequestDependency()).ToList(),
                requestNecessities.Select(m => m.GetRequestDependency()).ToList(),

                requestQualifications.Select(m => m.GetRequestDependency()).ToList(),
                requestPoints.Select(m => m.PointId).ToList(),
                cmd.RequestExpiredDate,
                cmd.RequestStartDate, request.FullOnline,
                request.Description, request.Regulation,
                requestConnection.Select(m => m.GetRequestDependency()).ToList(),
                request.RequestRequestOrgs?.Select(m => m.RequestId).ToList(),
                request.RequestDefiniteRequestOrgs?.Select(m => m.DefiniteRequestOrgId).ToList(),
                true);

            await _publisher.PublishAsync(@event);

            var @secondEvent=new AnnouncementAssignRequestEvent(request.Id, cmd.AnnouncementId);
            await _publisher.PublishAsync(@secondEvent);
            // _requestRepository.Add();
            return new CreateRequestCommandResult()
            {
                Id = request.Id
            };
        }
    }
}