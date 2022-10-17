using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdateSubjectCommandHandler : IBaseAsyncCommandHandler<UpdateSubjectCommand, UpdateSubjectCommandResult>
    {
        private readonly IGenericRepository<Subject> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdateSubjectCommandHandler(IGenericRepository<Subject> repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<UpdateSubjectCommandResult> HandleAsync(UpdateSubjectCommand cmd)
        {
            var subject = await _repository.Find(cmd.Id);
            subject.Update(cmd.Title, cmd.Icon);
            _repository.Update(subject);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new SubjectCretedOrUpdated(subject.Id, subject.Title,subject.Icon, subject.ParentId, false));

            return new UpdateSubjectCommandResult()
            {
                Id = subject.Id,
                ParentId = subject.ParentId,
                Title = subject.Title
            };
        }
    }
}