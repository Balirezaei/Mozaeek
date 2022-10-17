using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;

namespace MozaeekCore.ApplicationService.Command
{
    public class AddingToTechnicianEducationalInfoCommandHandler : IBaseAsyncCommandHandler<AddingToTechnicianEducationalInfoCommand, RegisterTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;
        private readonly IGenericRepository<EducationField> _educationfieldRepository;
        private readonly IGenericRepository<EducationGrade> _educationgradeRepository;
        public AddingToTechnicianEducationalInfoCommandHandler(ITechnicianRepository repository,
            IUnitOfWork unitOfWork,
            IMessagePublisher publisher, IGenericRepository<EducationField> educationfieldRepository, IGenericRepository<EducationGrade> educationgradeRepository)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._publisher = publisher;
            _educationfieldRepository = educationfieldRepository;
            _educationgradeRepository = educationgradeRepository;
        }

        public async Task<RegisterTechnicianCommandResult> HandleAsync(AddingToTechnicianEducationalInfoCommand cmd)
        {
            var technician = await _repository.FindWithEducationInfo(cmd.TechnicianId);
            var educationField = await _educationfieldRepository.Find(cmd.EducationFieldId);
            var educationGrade = await _educationgradeRepository.Find(cmd.EducationGradeId);
            var info = new TechnicianEducationalInfo(educationGrade.Id, educationField.Id);
            technician.AppendEducationalInfo(info);

            await _unitOfWork.CommitAsync();

            await _publisher.PublishAsync(
                new TechnicianEducationInfoAdded(technician.Id,
                    educationGrade.Id,
                    educationGrade.Title,
                    educationField.Id,
                    educationField.Title));

            return new RegisterTechnicianCommandResult()
            {
                Id = cmd.TechnicianId
            };
        }
    }
}