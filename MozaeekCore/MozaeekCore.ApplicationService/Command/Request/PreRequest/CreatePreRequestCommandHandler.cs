using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreatePreRequestCommandHandler : IBaseAsyncCommandHandler<CreatePreRequestCommand, CreatePreRequestCommandResult>
    {

        private readonly IPreRequestRepository _preRequestRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public CreatePreRequestCommandHandler(IPreRequestRepository preRequestRepository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _preRequestRepository = preRequestRepository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }
        public async Task<CreatePreRequestCommandResult> HandleAsync(CreatePreRequestCommand cmd)
        {
            var domain = new PreRequest(cmd.Title, cmd.Summary);
            _preRequestRepository.Add(domain);
            await _unitOfWork.CommitAsync();

            await _publisher.PublishAsync(new PreRequestCreated(domain.Id, domain.CreateDateTime, domain.Title, domain.Summary, domain.IsProcessed));

            return new CreatePreRequestCommandResult()
            {
                Id = domain.Id
            };
        }
    }
}