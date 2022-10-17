using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;

namespace MozaeekCore.ApplicationService.Command
{
    public class AddingToTechnicianRequestCommandHandler : IBaseAsyncCommandHandler<AddingToTechnicianRequestCommand, RegisterTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public AddingToTechnicianRequestCommandHandler(ITechnicianRepository repository,
            IUnitOfWork unitOfWork,
            IMessagePublisher publisher)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._publisher = publisher;
        }

        public async Task<RegisterTechnicianCommandResult> HandleAsync(AddingToTechnicianRequestCommand cmd)
        {
            var technician = await _repository.FindWithRequest(cmd.TechnicianId);
            var requests = cmd.RequestId.Select(m => new TechnicianRequest
            {
                TechnicianId = cmd.TechnicianId,
                RequestId = m
            });

            technician.AppendRequests(requests);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new TechnicianRequestAdded(technician.Id,
                requests.Select(m => m.RequestId).ToList()));
            await _publisher.PublishAsync(
                new TechnicianRequestAdded(technician.Id,
                    requests.Select(m => m.RequestId).ToList()));

            return new RegisterTechnicianCommandResult()
            {
                Id = cmd.TechnicianId
            };

        }
    }
}