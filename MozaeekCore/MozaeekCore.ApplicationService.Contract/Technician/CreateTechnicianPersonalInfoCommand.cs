using Mozaeek.CR.PublicDto;
using MozaeekCore.Core.Base;
using MozaeekCore.Enum;

namespace MozaeekCore.ApplicationService.Contract
{
    public class CreateTechnicianPersonalInfoCommand : Command
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        /// <summary>
        /// شماره شناسنامه
        /// </summary>
        public string IdentityNumber { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public TechnicianAttachmentDto Attachment { get; set; }
    }
}