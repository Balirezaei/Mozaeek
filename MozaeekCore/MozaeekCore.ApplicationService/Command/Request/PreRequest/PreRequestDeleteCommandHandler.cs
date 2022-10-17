using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class PreRequestDeleteCommandHandler : IBaseAsyncCommandHandler<PreRequestDeleteCommand, DeleteCommandResult>
    {
        private readonly IPreRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public PreRequestDeleteCommandHandler(IPreRequestRepository repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(PreRequestDeleteCommand cmd)
        {
            var unProcessedRequest = await _repository.Find(cmd.Id);
            _repository.Delete(unProcessedRequest);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new PreRequestDeleted(cmd.Id));

            return new DeleteCommandResult()
            {

            };
        }
    }
}