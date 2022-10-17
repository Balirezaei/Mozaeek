using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeletePropertyCommandHandler : IBaseAsyncCommandHandler<DeletePropertyCommand, DeleteCommandResult>
    {
        private readonly IGenericRepository<Property> _repository;
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IMessagePublisher _publisher;

        public DeletePropertyCommandHandler(IGenericRepository<Property> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeletePropertyCommand cmd)
        {
            var Property = await _repository.Find(cmd.Id);
            _repository.Delete(Property);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new PropertyDeleted(cmd.Id));

            return new DeleteCommandResult()
            {
            };
        }
    }
}