using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command.BasicInfo
{
    public class CreateDefiniteRequestOrgCommandHandler : IBaseAsyncCommandHandler<CreateDefiniteRequestOrgCommand, CreateDefiniteRequestOrgCommandResult>
    {
        private readonly IGenericRepository<DefiniteRequestOrg> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public CreateDefiniteRequestOrgCommandHandler(IGenericRepository<DefiniteRequestOrg> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<CreateDefiniteRequestOrgCommandResult> HandleAsync(CreateDefiniteRequestOrgCommand cmd)
        {
            var domain =
                new DefiniteRequestOrg(0, cmd.RequestOrgId, cmd.PointId, cmd.Address, cmd.PhoneNumber);
            await _repository.Add(domain);
            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new DefiniteRequestOrgCretedOrUpdated(domain.Id, domain.RequestOrgId, domain.PointId,
                  domain.Address, domain.PhoneNumber, true));
            return new CreateDefiniteRequestOrgCommandResult()
            {
            };
        }
    }
}