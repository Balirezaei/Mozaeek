using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteRequestActCommandHandler : IBaseAsyncCommandHandler<DeleteRequestActCommand, DeleteCommandResult>
    {
        private readonly IGenericRepository<RequestAct> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public DeleteRequestActCommandHandler(IGenericRepository<RequestAct> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteRequestActCommand cmd)
        {
            var obj = await _repository.Find(cmd.Id);
            _repository.Delete(obj);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestActDeleted(obj.Id));

            return new DeleteCommandResult()
            {
            };
        }
    }
}