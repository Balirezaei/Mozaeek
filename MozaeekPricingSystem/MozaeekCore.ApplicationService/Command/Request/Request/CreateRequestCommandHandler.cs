using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.Mapper;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRequestCommandHandler : IBaseAsyncCommandHandler<CreateRequestCommand, CreateRequestCommandResult>
    {
        private readonly IRequestRepository _requestRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;
        public CreateRequestCommandHandler(IRequestRepository requestRepository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _requestRepository = requestRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<CreateRequestCommandResult> HandleAsync(CreateRequestCommand cmd)
        {
            var requestPoints = cmd.Points.Select(m => m.Id).Select(m => new RequestPoint()
            {
                PointId = m
            }).ToList();

            var requestActions = cmd.RequestActions.Select(m => new RequestAction(0, m.Description, m.Priority))
                .ToList();

            var requestNessesitys = cmd.RequestNessesities
                .Select(m => new RequestNessesity(0, m.Description, m.Priority)).ToList();

            var requestDocuments = cmd.RequestDocuments
                .Select(m => new RequestDocument(0, m.Description, m.Priority)).ToList();

            var requestQualifications = cmd.RequestQualifications
                .Select(m => new RequestQualification(0, m.Description, m.Priority)).ToList();

            var request = new Request(cmd.RequestTargetId, cmd.RequestActId,
                requestActions,
                requestNessesitys,
                requestDocuments,
                requestQualifications,
                requestPoints,
                cmd.RequestExpiredDate,
                cmd.RequestStartDate);

            _requestRepository.Add(request);
            await _unitOfWork.CommitAsync();

            var @event = new RequestCretedOrUpdated(
                request.Id
                , cmd.RequestTargetId,
                cmd.RequestActId,
                requestActions.Select(m => m.GetRequestDependency()).ToList(),
                requestNessesitys.Select(m => m.GetRequestDependency()).ToList(),
                requestDocuments.Select(m => m.GetRequestDependency()).ToList(),
                requestQualifications.Select(m => m.GetRequestDependency()).ToList(),
                requestPoints.Select(m => m.PointId).ToList(),
                cmd.RequestExpiredDate,
                cmd.RequestStartDate, true);

            await _publisher.PublishAsync(@event);

            // _requestRepository.Add();
            return new CreateRequestCommandResult()
            {
                Id = request.Id
            };
        }
    }

}