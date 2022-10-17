using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Core.MessagePublisher;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Contract;
using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Command
{
    public class UpdateTechnicianPersonalInfoCommandHandler : IBaseAsyncCommandHandler<UpdateTechnicianPersonalInfoCommand, RegisterTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _publisher;

        public UpdateTechnicianPersonalInfoCommandHandler(ITechnicianRepository repository,
            IUnitOfWork unitOfWork,
            IMessagePublisher publisher)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._publisher = publisher;
        }

        public async Task<RegisterTechnicianCommandResult> HandleAsync(UpdateTechnicianPersonalInfoCommand cmd)
        {
            var technician = await _repository.FindWithPersonalInfo(cmd.TechnicianId);

            var personalInfo = new TechnicianPersonalInfo(0, cmd.FirstName, cmd.LastName, cmd.NationalCode,
                cmd.IdentityNumber);

            if (cmd.Attachment != null)
            {
                var attachment = new TechnicianAttachment(0, AttachmentType.PersonalPhoto, cmd.Attachment.Source,
                    cmd.Attachment.FileName, cmd.Attachment.FileExtention);
                attachment.Technician = technician;
                await _repository.ReplacePersonalPhoto(technician.Id, attachment);
            }

            technician.UpdatePersonalInfo(personalInfo);

            await _unitOfWork.CommitAsync();

            await _publisher.PublishAsync(new TechnicianPersonalInfoCreatedOrUpdated(technician.Id,
                personalInfo.FirstName,
                personalInfo.LastName,
                personalInfo.NationalCode,
                personalInfo.IdentityNumber,
                cmd.TechnicianType,
                technician.CreateDateTime,
                false));

            return new RegisterTechnicianCommandResult()
            {
                Id = technician.Id
            };
        }
    }
}