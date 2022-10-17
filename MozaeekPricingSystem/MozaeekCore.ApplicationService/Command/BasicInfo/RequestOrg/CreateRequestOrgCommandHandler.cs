using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRequestOrgCommandHandler : IBaseAsyncCommandHandler<CreateRequestOrgCommand, CreateRequestOrgCommandResult>
    {

        private readonly IGenericRepository<RequestOrg> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public CreateRequestOrgCommandHandler(IGenericRepository<RequestOrg> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }
        public async Task<CreateRequestOrgCommandResult> HandleAsync(CreateRequestOrgCommand cmd)
        {
            var requestOrg = new RequestOrg(0, cmd.Title, cmd.ParentId);
            _repository.Add(requestOrg);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestOrgCretedOrUpdated(requestOrg.Id, requestOrg.Title, requestOrg.ParentId, true));

            return new CreateRequestOrgCommandResult()
            {
                Id = requestOrg.Id,
                Title = requestOrg.Title
            };
        }
    }
}