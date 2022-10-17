using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.Commands;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.Request.Events;
using MozaeekCore.Exceptions;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteRequestCommandHandler : IBaseAsyncCommandHandler<DeleteRequestCommand, DeleteCommandResult>
    {
        private readonly IRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public DeleteRequestCommandHandler(IRequestRepository repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteRequestCommand cmd)
        {
            var request = await _repository.Find(cmd.Id);
            _repository.Delete(request);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestDeleted(request.Id));

            return new DeleteCommandResult()
            {
            };
        }
    }
}