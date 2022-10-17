using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Command
{
    public class CreateTechnicianPersonalInfoCommandHandler : IBaseAsyncCommandHandler<CreateTechnicianPersonalInfoCommand, RegisterTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public CreateTechnicianPersonalInfoCommandHandler(ITechnicianRepository repository,
                                                         IUnitOfWork unitOfWork,
                                                         IMessagePublisher publisher)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._publisher = publisher;
        }

        public async Task<RegisterTechnicianCommandResult> HandleAsync(CreateTechnicianPersonalInfoCommand cmd)
        {
            var technician = new Technician(0, cmd.TechnicianType);

            var personalInfo = new TechnicianPersonalInfo(0, cmd.FirstName, cmd.LastName, cmd.NationalCode,
                cmd.IdentityNumber);

            if (cmd.Attachment != null)
            {
                var attachment = new TechnicianAttachment(0, AttachmentType.PersonalPhoto, cmd.Attachment.Source,
                    cmd.Attachment.FileName, cmd.Attachment.FileExtention);
                attachment.Technician = technician;
                technician.AddAttachment(attachment);
            }

            technician.AddPersonalInfo(personalInfo);
            _repository.Add(technician);

            await _unitOfWork.CommitAsync();

            await _publisher.PublishAsync(new TechnicianPersonalInfoCreatedOrUpdated(technician.Id,
                personalInfo.FirstName,
                personalInfo.LastName,
                personalInfo.NationalCode,
                personalInfo.IdentityNumber,
                cmd.TechnicianType,
                technician.CreateDateTime,
               true));

            return new RegisterTechnicianCommandResult()
            {
                Id = technician.Id
            };
        }
    }
}
