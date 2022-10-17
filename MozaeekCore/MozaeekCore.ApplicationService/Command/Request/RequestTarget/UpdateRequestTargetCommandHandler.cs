using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdateRequestTargetCommandHandler : IBaseAsyncCommandHandler<UpdateRequestTargetCommand, UpdateRequestTargetCommandResult>
    {
        private readonly IRequestTargetRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdateRequestTargetCommandHandler(IRequestTargetRepository repository, IUnitOfWork unitOfWork, IMessagePublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task<UpdateRequestTargetCommandResult> HandleAsync(UpdateRequestTargetCommand cmd)
        {
            var requestTargetSubjects = cmd.Subjects.Select(m => m.Id).Select(m => new RequestTargetSubject()
            {
                SubjectId = m
            }).ToList();

            //var requestTargetRequestOrgs = cmd.RequestOrgs.Select(m => m.Id).Select(m => new RequestTargetRequestOrg()
            //{
            //    RequestOrgId = m
            //}).ToList();

            var requestTargetLabels = cmd.Labels.Select(m => m.Id).Select(m => new RequestTargetLabel()
            {
                LabelId = m
            }).ToList();


            var requestTarget = await _repository.Find(cmd.Id);


            _repository.ResetAssociations(requestTarget);
            requestTarget.Update(cmd.Title, requestTargetSubjects, requestTargetLabels,  cmd.IsDocument,cmd.Icon);

            _repository.UpdateRequestTarget(requestTarget);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestTargetCreatedOrUpdated(requestTarget.Id,
                requestTarget.Title, requestTarget.Icon,
                requestTargetSubjects.Select(m => m.SubjectId).ToList(),
                requestTargetLabels.Select(m => m.LabelId).ToList(), cmd.IsDocument,
                false
            ));

            return new UpdateRequestTargetCommandResult()
            {
                Id = requestTarget.Id,
                Title = requestTarget.Title
            };
        }
    }
}