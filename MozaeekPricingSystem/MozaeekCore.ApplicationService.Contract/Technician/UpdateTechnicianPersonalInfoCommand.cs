using MozaeekCore.Core.Base;
using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract
{
    public class UpdateTechnicianPersonalInfoCommand : Command
    {
        public long TechnicianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// کدملی
        /// </summary>
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        public string IdentityNumber { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public TechnicianAttachmentDto Attachment { get; set; }
    }
}