using MozaeekCore.Core.Base;
using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract
{
    public class AddingToTechnicianRequestCommand : Command
    {
        public long[] RequestId { get; set; }
        public long TechnicianId { get; set; }
    }

    public class AddingToTechnicianAttachmentCommand : Command
    {
        public AddingToTechnicianAttachmentCommand(long technicianId, TechnicianAttachmentDto attachmentDto)
        {
            TechnicianId = technicianId;
            AttachmentDto = attachmentDto;
        }

        public long TechnicianId { get; set; }
        public TechnicianAttachmentDto AttachmentDto { get; set; }
    }
    public class RemovingTechnicianAttachmentCommand : Command
    {
        public long TechnicianId { get; set; }
        public long AttachmentHd { get; set; }
    }
}