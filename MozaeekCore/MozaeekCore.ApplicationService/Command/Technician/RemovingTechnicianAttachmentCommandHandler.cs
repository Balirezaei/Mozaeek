using System.Linq;
using System.Threading.Tasks;
using MozaeekCore.ApplicationService.Contract;
using MozaeekCore.Core;
using MozaeekCore.Core.CommandHandler;
using MozaeekCore.Domain;

namespace MozaeekCore.ApplicationService.Command
{
    //public class RemovingTechnicianAttachmentCommandHandler : IBaseAsyncCommandHandler<RemovingTechnicianAttachmentCommand, RegisterTechnicianCommandResult>
    //{
    //    private readonly ITechnicianRepository _repository;
    //    private readonly IUnitOfWork _unitOfWork;

    //    public RemovingTechnicianAttachmentCommandHandler(ITechnicianRepository repository,
    //        IUnitOfWork unitOfWork)
    //    {
    //        this._repository = repository;
    //        this._unitOfWork = unitOfWork;
    //    }
    //    public async Task<RegisterTechnicianCommandResult> HandleAsync(RemovingTechnicianAttachmentCommand cmd)
    //    {
    //        var technician = await _repository.FindWithAttachment(cmd.TechnicianId);
    //        var attachment = technician.TechnicianAttachments.FirstOrDefault(m => m.Id == cmd.AttachmentHd);

    //        technician.TechnicianAttachments.Remove(attachment);
    //        await _unitOfWork.CommitAsync();

    //        return new RegisterTechnicianCommandResult()
    //        {
    //            Id = technician.Id
    //        };
    //    }
    //}
}