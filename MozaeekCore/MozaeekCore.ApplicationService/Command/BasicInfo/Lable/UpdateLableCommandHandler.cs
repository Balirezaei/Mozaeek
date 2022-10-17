using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdateLabelCommandHandler : IBaseAsyncCommandHandler<UpdateLabelCommand, UpdateLabelCommandResult>
    {
        private readonly IGenericRepository<Label> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdateLabelCommandHandler(IGenericRepository<Label> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<UpdateLabelCommandResult> HandleAsync(UpdateLabelCommand cmd)
        {
            var label = await _repository.Find(cmd.Id);
            label.UpdateTitle(cmd.Title);
            _repository.Update(label);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new LabelCreatedOrUpdated(label.Id, label.Title, label.ParentId, false));
            
            return new UpdateLabelCommandResult()
            {
                Id = label.Id,
                ParentId = label.ParentId,
                Title = label.Title
            };
        }
    }
}