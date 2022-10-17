using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Persistense.MongoDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.ApplicationService.Command.BasicInfo.Point
{
    public class DeleteAllPointCommandHandler : IBaseAsyncCommandHandler<MozaeekCore.Core.Base.Command, DeleteCommandResult>
    {
        private readonly IGenericRepository<Domain.Point> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPointQueryService _pointQueryService;

        public DeleteAllPointCommandHandler(IGenericRepository<Domain.Point> repository, IUnitOfWork unitOfWork, IPointQueryService pointQueryService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _pointQueryService = pointQueryService;
        }

        public async Task<DeleteCommandResult> HandleAsync(MozaeekCore.Core.Base.Command cmd)
        {
            _repository.DeleteAll();
            await _unitOfWork.CommitAsync();
            await _pointQueryService.RemoveAll();
            _repository.ReSeed();
            return new DeleteCommandResult()
            {
            };
        }
    
    
    }
}
