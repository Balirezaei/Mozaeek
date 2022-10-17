using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdateRSSCommandHandler : IBaseAsyncCommandHandler<UpdateRSSCommand, UpdateRSSCommandResult>
    {
        private readonly IGenericRepository<RSS> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdateRSSCommandHandler(IGenericRepository<RSS> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<UpdateRSSCommandResult> HandleAsync(UpdateRSSCommand cmd)
        {
            var rss = await _repository.Find(cmd.Id);
            rss.Update(cmd.Url,cmd.Source,cmd.IsActive,cmd.IntervalDataReceiveHours);
            _repository.Update(rss);

            await _unitOfWork.CommitAsync();
            
            return new UpdateRSSCommandResult()
            {
                Id = rss.Id,
                Source = rss.Source,
                IsActive = rss.IsActive,
                Url = rss.Url
            };
        }
    }
}