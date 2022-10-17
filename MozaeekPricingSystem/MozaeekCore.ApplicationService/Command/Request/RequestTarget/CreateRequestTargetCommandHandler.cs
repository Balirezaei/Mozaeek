using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract.BasicInfo;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateRequestTargetCommandHandler : IBaseAsyncCommandHandler<CreateRequestTargetCommand, CreateRequestTargetCommandResult>
    {
        private readonly IRequestTargetRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;
        private readonly IGenericRepository<Subject> _subjectRepository;
        private readonly IGenericRepository<RequestOrg> _requestOrgRepository;

        public CreateRequestTargetCommandHandler(IRequestTargetRepository repository, IUnitOfWork unitOfWork,
            IMessagePublisher publisher, IGenericRepository<Subject> subjectRepository,
            IGenericRepository<RequestOrg> requestOrgRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
            _subjectRepository = subjectRepository;
            _requestOrgRepository = requestOrgRepository;
        }

        public async Task<CreateRequestTargetCommandResult> HandleAsync(CreateRequestTargetCommand cmd)
        {
            var requestTargetSubjects = cmd.Subjects.Select(m => m.Id).Select(m => new RequestTargetSubject()
            {
                SubjectId = m
            }).ToList();

            var requestTargetRequestOrgs = cmd.RequestOrgs.Select(m => m.Id).Select(m => new RequestTargetRequestOrg()
            {
                RequestOrgId = m
            }).ToList();

            var requestTargetLabels = cmd.Labels.Select(m => m.Id).Select(m => new RequestTargetLabel()
            {
                LabelId = m
            }).ToList();

            var requestTarget = new RequestTarget(0, cmd.Title, requestTargetSubjects, requestTargetLabels, requestTargetRequestOrgs);
            await _repository.CreatRequestTarget(requestTarget);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(new RequestTargetCreatedOrUpdated(requestTarget.Id,
                requestTarget.Title,
                requestTargetSubjects.Select(m => m.SubjectId).ToList(),
                requestTargetLabels.Select(m => m.LabelId).ToList(),
                requestTargetRequestOrgs.Select(m => m.RequestOrgId).ToList(),
                true
            ));

            return new CreateRequestTargetCommandResult()
            {
                Id = requestTarget.Id,
            };
        }
    }
}