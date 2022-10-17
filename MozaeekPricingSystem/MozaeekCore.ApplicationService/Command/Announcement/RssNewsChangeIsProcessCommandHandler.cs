using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core.CommandHandler;
using System.Threading.Tasks;
using MozaeekCore.Core;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Command
{
    public class RssNewsChangeIsProcessCommandHandler : IBaseAsyncCommandHandler<RssNewsChangeIsProcessCommand, Nothing>
    {
        private readonly IAnnouncementRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RssNewsChangeIsProcessCommandHandler(IAnnouncementRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Nothing> HandleAsync(RssNewsChangeIsProcessCommand cmd)
        {
            _repository.UpdateNewsRssState(cmd.NewsId);
            await _unitOfWork.CommitAsync();
            return new Nothing();
        }
    }
}
