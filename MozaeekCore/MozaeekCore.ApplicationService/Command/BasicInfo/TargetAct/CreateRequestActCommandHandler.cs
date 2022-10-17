using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRequestActCommandHandler : IBaseAsyncCommandHandler<CreateRequestActCommand, CreateRequestActCommandResult>
    {

        private readonly IGenericRepository<RequestAct> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public CreateRequestActCommandHandler(IGenericRepository<RequestAct> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }
        public async Task<CreateRequestActCommandResult> HandleAsync(CreateRequestActCommand cmd)
        {
            var requestAct = new RequestAct(0, cmd.Title);
            await _repository.Add(requestAct);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestActCretedOrUpdated(requestAct.Id, requestAct.Title, true));

            return new CreateRequestActCommandResult()
            {
                Id = requestAct.Id,
                Title = requestAct.Title
            };
        }
    }
}