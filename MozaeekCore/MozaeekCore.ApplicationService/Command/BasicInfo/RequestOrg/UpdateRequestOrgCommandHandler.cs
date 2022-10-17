using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdateRequestOrgCommandHandler : IBaseAsyncCommandHandler<UpdateRequestOrgCommand, UpdateRequestOrgCommandResult>
    {
        private readonly IGenericRepository<RequestOrg> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdateRequestOrgCommandHandler(IGenericRepository<RequestOrg> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<UpdateRequestOrgCommandResult> HandleAsync(UpdateRequestOrgCommand cmd)
        {
            var requestOrg = await _repository.Find(cmd.Id);
            requestOrg.Update(cmd.Title,cmd.Icon);
            _repository.Update(requestOrg);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestOrgCretedOrUpdated(requestOrg.Id, requestOrg.Title,requestOrg.Icon, requestOrg.ParentId, false));

            return new UpdateRequestOrgCommandResult()
            {
                Id = requestOrg.Id,
                ParentId = requestOrg.ParentId,
                Title = requestOrg.Title
            };
        }
    }
}