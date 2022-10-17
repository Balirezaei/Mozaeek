using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteLabelCommandHandler : IBaseAsyncCommandHandler<DeleteLabelCommand, DeleteCommandResult>
    {
        private readonly IGenericRepository<Label> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public DeleteLabelCommandHandler(IGenericRepository<Label> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteLabelCommand cmd)
        {
            var Label = await _repository.Find(cmd.Id);
            _repository.Delete(Label);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new LabelDeleted(cmd.Id));


            return new DeleteCommandResult()
            {
            };
        }
    }
}