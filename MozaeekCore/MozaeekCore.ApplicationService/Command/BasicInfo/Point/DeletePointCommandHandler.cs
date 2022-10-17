using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeletePointCommandHandler : IBaseAsyncCommandHandler<DeletePointCommand, DeleteCommandResult>
    {
        private readonly IGenericRepository<Point> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public DeletePointCommandHandler(IGenericRepository<Point> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeletePointCommand cmd)
        {
            var point = await _repository.Find(cmd.Id);
            _repository.Delete(point);
            await _unitOfWork.CommitAsync();

            await _publisher.PublishAsync(new PointDeleted(cmd.Id));

            return new DeleteCommandResult()
            {
            };
        }
    }
}