using MozaeekCore.Common.ExtensionMethod;
using MozaeekCore.Enum;

namespace MozaeekCore.Domain
{
    /// <summary>
    /// اتچمنت مدارک
    /// </summary>
    public class TechnicianAttachment
    {
        public long Id { get; set; }
        public MosaikFile File { get; set; }
        public long FileId { get; set; }

        public Technician Technician { get; set; }
        public long TechnicianId { get; set; }
    }
}