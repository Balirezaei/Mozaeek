using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.ApplicationService.Contract.Announcement;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Command
{
    // public class RssNewsChangeIsRequestCommandHandler : IBaseAsyncCommandHandler<RssNewsChangeIsRequestCommand, Nothing>
    // {
    //     private readonly IAnnouncementRepository _repository;
    //     private readonly IUnitOfWork _unitOfWork;
    //
    //     public RssNewsChangeIsRequestCommandHandler(IAnnouncementRepository repository, IUnitOfWork unitOfWork)
    //     {
    //         _repository = repository;
    //         _unitOfWork = unitOfWork;
    //     }
    //
    //     public async Task<Nothing> HandleAsync(RssNewsChangeIsRequestCommand cmd)
    //     {
    //         _repository.UpdateNewsIsRequest(cmd.NewsId);
    //         await _unitOfWork.CommitAsync();
    //         return new Nothing();
    //     }
    // }
}