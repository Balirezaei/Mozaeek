using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdateRequestActCommandHandler : IBaseAsyncCommandHandler<UpdateRequestActCommand, RequestActRequestCommandResult>
    {
        private readonly IGenericRepository<RequestAct> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdateRequestActCommandHandler(IGenericRepository<RequestAct> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<RequestActRequestCommandResult> HandleAsync(UpdateRequestActCommand cmd)
        {
            var requestAct = await _repository.Find(cmd.Id);
            requestAct.UpdateTitle(cmd.Title);
            _repository.Update(requestAct);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestActCretedOrUpdated(requestAct.Id, requestAct.Title, false));

            return new RequestActRequestCommandResult()
            {
                Id = requestAct.Id,
                Title = requestAct.Title
            };
        }
    }
}