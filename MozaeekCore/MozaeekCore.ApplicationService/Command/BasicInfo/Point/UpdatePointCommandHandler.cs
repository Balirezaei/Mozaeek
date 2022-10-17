using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdatePointCommandHandler : IBaseAsyncCommandHandler<UpdatePointCommand, UpdatePointCommandResult>
    {
        private readonly IGenericRepository<Point> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdatePointCommandHandler(IGenericRepository<Point> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<UpdatePointCommandResult> HandleAsync(UpdatePointCommand cmd)
        {
            var point = await _repository.Find(cmd.Id);
            point.UpdateTitle(cmd.Title);
            _repository.Update(point);
            

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new PointCreatedOrUpdated(point.Id, point.Title, point.ParentId, false));
            
            return new UpdatePointCommandResult()
            {
                Id = point.Id,
                ParentId = point.ParentId,
                Title = point.Title
            };
        }
    }
}