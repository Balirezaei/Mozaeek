using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;

namespace MozaeekCore.ApplicationService.Command
{
    public class AddingToTechnicianSubjectCommandHandler : IBaseAsyncCommandHandler<AddingToTechnicianSubjectCommand, RegisterTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public AddingToTechnicianSubjectCommandHandler(ITechnicianRepository repository,
            IUnitOfWork unitOfWork,
            IMessagePublisher publisher)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._publisher = publisher;
        }

        public async Task<RegisterTechnicianCommandResult> HandleAsync(AddingToTechnicianSubjectCommand cmd)
        {
            var technician = await _repository.FindWithSubject(cmd.TechnicianId);
            var subjects = cmd.SubjectId.Select(m => new TechnicianSubject
            {
                TechnicianId = cmd.TechnicianId,
                SubjectId = m
            });
           
            technician.AppendSubjects(subjects);

            await _unitOfWork.CommitAsync();
            await _publisher.PublishAsync(
                new TechnicianSubjectAdded(technician.Id,
                    subjects.Select(m => m.SubjectId).ToList()));

            return new RegisterTechnicianCommandResult()
            {
                Id = cmd.TechnicianId
            };

        }
    }
}