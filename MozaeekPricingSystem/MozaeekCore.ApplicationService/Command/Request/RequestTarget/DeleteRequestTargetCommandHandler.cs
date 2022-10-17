using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteRequestTargetCommandHandler : IBaseAsyncCommandHandler<DeleteRequestTargetCommand, DeleteCommandResult>
    {
        private readonly IRequestTargetRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public DeleteRequestTargetCommandHandler(IRequestTargetRepository repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteRequestTargetCommand cmd)
        {
            var requestTarget = await _repository.Find(cmd.Id);
            _repository.Delete(requestTarget);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestTargetDeleted(requestTarget.Id));

            return new DeleteCommandResult()
            {
            };
        }
    }
}