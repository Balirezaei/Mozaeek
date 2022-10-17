using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command.BasicInfo
{
    public class DeleteDefiniteRequestOrgCommandHandler : IBaseAsyncCommandHandler<DeleteDefiniteRequestOrgCommand, Nothing>
    {
        private readonly IGenericRepository<DefiniteRequestOrg> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public DeleteDefiniteRequestOrgCommandHandler(IGenericRepository<DefiniteRequestOrg> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<Nothing> HandleAsync(DeleteDefiniteRequestOrgCommand cmd)
        {
            var domain = await _repository.Find(cmd.Id);
            _repository.Delete(domain);
            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new DefiniteRequestOrgDeleted(domain.Id));
            return new Nothing();
        }
    }
}