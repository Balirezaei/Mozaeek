using System.Threading.Tasks;
using System.Transactions;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateSubjectCommandHandler : IBaseAsyncCommandHandler<CreateSubjectCommand, CreateSubjectCommandResult>
    {
        private readonly IGenericRepository<Subject> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public CreateSubjectCommandHandler(IGenericRepository<Subject> repository,
            IUnitOfWork unitOfWork,
            IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }
        public async Task<CreateSubjectCommandResult> HandleAsync(CreateSubjectCommand cmd)
        {
            //TODO : Decide about the Id 
            var subject = new Subject(0, cmd.Title, cmd.Icon, cmd.ParentId);
            await _repository.Add(subject);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new SubjectCretedOrUpdated(subject.Id, subject.Title,subject.Icon, subject.ParentId, true));


            return new CreateSubjectCommandResult()
            {
                Id = subject.Id,
                ParentId = subject.ParentId,
                Title = subject.Title
            };
        }
    }
}