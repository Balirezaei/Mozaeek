using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.CommandHandler;
using System.Threading.Tasks;
using MozaeekCore.Core;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreatePointCommandHandler : IBaseAsyncCommandHandler<CreatePointCommand, CreatePointCommandResult>
    {
        private readonly IGenericRepository<Point> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public CreatePointCommandHandler(IGenericRepository<Point> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<CreatePointCommandResult> HandleAsync(CreatePointCommand cmd)
        {
            var point = new Point(0,cmd.Title, cmd.ParentId);
            _repository.Add(point);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new PointCreatedOrUpdated(point.Id, point.Title, point.ParentId, true));

            return new CreatePointCommandResult()
            {
                Id = point.Id,
                ParentId = point.ParentId,
                Title = point.Title
            };
        }
    }
}