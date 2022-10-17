using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteRequestOrgCommandHandler : IBaseAsyncCommandHandler<DeleteRequestOrgCommand, DeleteCommandResult>
    {
        private readonly IGenericRepository<RequestOrg> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public DeleteRequestOrgCommandHandler(IGenericRepository<RequestOrg> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteRequestOrgCommand cmd)
        {
            var requestOrg = await _repository.Find(cmd.Id);
            _repository.Delete(requestOrg);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestOrgDeleted(requestOrg.Id));

            return new DeleteCommandResult()
            {
            };
        }
    }
}