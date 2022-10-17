using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command.BasicInfo
{
    public class UpdateDefiniteRequestOrgCommandHandler : IBaseAsyncCommandHandler<UpdateDefiniteRequestOrgCommand, UpdateDefiniteRequestOrgCommandResult>
    {
        private readonly IGenericRepository<DefiniteRequestOrg> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdateDefiniteRequestOrgCommandHandler(IMessagePublisher publisher, IUnitOfWork unitOfWork, IGenericRepository<DefiniteRequestOrg> repository)
        {
            _publisher = publisher;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<UpdateDefiniteRequestOrgCommandResult> HandleAsync(UpdateDefiniteRequestOrgCommand cmd)
        {
            var domain = await _repository.Find(cmd.Id);
            domain.Update(cmd.Address, cmd.PhoneNumber, cmd.PointId);
            _repository.Update(domain);
            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new DefiniteRequestOrgCretedOrUpdated(domain.Id, domain.RequestOrgId, domain.PointId,
                domain.Address, domain.PhoneNumber, false));
            return new UpdateDefiniteRequestOrgCommandResult()
            {

            };
        }
    }
}