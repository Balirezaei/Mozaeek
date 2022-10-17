using MozaeekCore.Common.ExtentionMethod;
using MozaeekCore.Enum;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// اتچمنت مدارک
    /// </summary>
    public class TechnicianAttachment
    {
        public TechnicianAttachment(long id, AttachmentType attachmentType, byte[] source, string fileName, string fileExtention)
        {
            Id = id;
            AttachmentType = attachmentType;
            Source = source;
            FileName = fileName.Recheck();
            FileExtention = fileExtention;
        }

        public long Id { get; set; }
        public AttachmentType AttachmentType { get; set; }
        public byte[] Source { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public long TechnicianId { get; set; }
        public virtual Technician Technician { get; set; }
    }
}