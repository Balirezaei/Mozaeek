using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Command
{
    public class UnProcessedRequestCommandHandler : IBaseAsyncCommandHandler<CreateUnProcessedRequestCommand, UnProcessedRequestCommandResult>
    {

        private readonly IUnProcessedRequestRepository _unProcessedRequestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnProcessedRequestCommandHandler(IUnProcessedRequestRepository unProcessedRequestRepository, IUnitOfWork unitOfWork)
        {
            _unProcessedRequestRepository = unProcessedRequestRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UnProcessedRequestCommandResult> HandleAsync(CreateUnProcessedRequestCommand cmd)
        {
            var unProcessedRequest = new UnProcessedRequest(cmd.Title, cmd.Summery);
            _unProcessedRequestRepository.Add(unProcessedRequest);
            await _unitOfWork.CommitAsync();
            return new UnProcessedRequestCommandResult()
            {
                Id = unProcessedRequest.Id
            };
        }
    }
}