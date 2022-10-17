using System.Threading.Tasks;
using System.Transactions;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateLabelCommandHandler : IBaseAsyncCommandHandler<CreateLabelCommand, CreateLabelCommandResult>
    {
        private readonly IGenericRepository<Label> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public CreateLabelCommandHandler(IGenericRepository<Label> repository,
                                         IUnitOfWork unitOfWork,
                                         IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }
        public async Task<CreateLabelCommandResult> HandleAsync(CreateLabelCommand cmd)
        {
            var label = new Label(0, cmd.Title, cmd.ParentId);

            await _repository.Add(label);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new LabelCreatedOrUpdated(label.Id, label.Title, label.ParentId, true));


            return new CreateLabelCommandResult()
            {
                Id = label.Id,
                ParentId = label.ParentId,
                Title = label.Title
            };
        }
    }
}