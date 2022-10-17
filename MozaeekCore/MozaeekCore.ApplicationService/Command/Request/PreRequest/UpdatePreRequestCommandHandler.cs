using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdatePreRequestCommandHandler : IBaseAsyncCommandHandler<UpdatePreRequestCommand, UpdatePreRequestCommandResult>
    {
        private readonly IPreRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdatePreRequestCommandHandler(IPreRequestRepository repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<UpdatePreRequestCommandResult> HandleAsync(UpdatePreRequestCommand cmd)
        {
            var domain = await _repository.Find(cmd.Id);
            domain.UpdateTitle(cmd.Title, cmd.Summary);
            _repository.Update(domain);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new PreRequestUpdated(domain.Id, domain.Title, domain.Summary, domain.IsProcessed));

            return new UpdatePreRequestCommandResult()
            {
                Id = domain.Id,
                Summery = domain.Summary,
                Title = domain.Title
            };
        }
    }
}