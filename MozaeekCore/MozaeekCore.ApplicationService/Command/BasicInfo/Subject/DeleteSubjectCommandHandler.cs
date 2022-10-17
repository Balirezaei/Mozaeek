using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class DeleteSubjectCommandHandler : IBaseAsyncCommandHandler<DeleteSubjectCommand, DeleteCommandResult>
    {
        private readonly IGenericRepository<Subject> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public DeleteSubjectCommandHandler(IGenericRepository<Subject> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<DeleteCommandResult> HandleAsync(DeleteSubjectCommand cmd)
        {
            var Subject = await _repository.Find(cmd.Id);
            _repository.Delete(Subject);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new SubjectDeleted(cmd.Id));


            return new DeleteCommandResult()
            {
            };
        }
    }
}