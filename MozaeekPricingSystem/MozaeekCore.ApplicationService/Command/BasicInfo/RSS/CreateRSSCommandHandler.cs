using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRSSCommandHandler : IBaseAsyncCommandHandler<CreateRSSCommand, CreateRSSCommandResult>
    {
        private readonly IGenericRepository<RSS> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRSSCommandHandler(IGenericRepository<RSS> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateRSSCommandResult> HandleAsync(CreateRSSCommand cmd)
        {
            var rss = new RSS(0,cmd.Url, cmd.Source,cmd.IntervalDataReceiveHours);
            _repository.Add(rss);
            
            await _unitOfWork.CommitAsync();

            return new CreateRSSCommandResult()
            {
                Id = rss.Id,
                Source = rss.Source,
                Url = rss.Url
            };
        }
    }
}