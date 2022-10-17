using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Command
{
    public class AddingToTechnicianAttachmentCommandHandler : IBaseAsyncCommandHandler<AddingToTechnicianAttachmentCommand, RegisterTechnicianCommandResult>
    {
        private readonly ITechnicianRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddingToTechnicianAttachmentCommandHandler(ITechnicianRepository repository,
            IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<RegisterTechnicianCommandResult> HandleAsync(AddingToTechnicianAttachmentCommand cmd)
        {
            var technician = await _repository.FindWithAttachment(cmd.TechnicianId);

            technician.TechnicianAttachments.Add(new TechnicianAttachment(0,
                cmd.AttachmentDto.AttachmentType,
                cmd.AttachmentDto.Source,
                cmd.AttachmentDto.FileName,
                cmd.AttachmentDto.FileExtention));

            await _unitOfWork.CommitAsync();

            return new RegisterTechnicianCommandResult()
            {
                Id = technician.Id
            };
        }
    }
}