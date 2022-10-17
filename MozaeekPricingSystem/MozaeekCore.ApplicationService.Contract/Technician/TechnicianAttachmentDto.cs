using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract
{
    public class TechnicianAttachmentDto
    {
        public long Id { get; set; }
        public AttachmentType AttachmentType { get; set; }
        public byte[] Source { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
    }
}