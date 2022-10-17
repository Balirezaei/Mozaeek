using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteRSSCommandHandler : IBaseAsyncCommandHandler<DeleteRSSCommand, DeleteCommandResult>
    {
        private readonly IGenericRepository<RSS> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRSSCommandHandler(IGenericRepository<RSS> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteRSSCommand cmd)
        {
            var RSS = await _repository.Find(cmd.Id);
            _repository.Delete(RSS);

            await _unitOfWork.CommitAsync();

            return new DeleteCommandResult()
            {
            };
        }
    }
}