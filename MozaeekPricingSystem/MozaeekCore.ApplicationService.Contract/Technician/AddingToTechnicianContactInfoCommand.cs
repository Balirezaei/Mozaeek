using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract
{
    public class AddingToTechnicianContactInfoCommand : Command
    {
        public long TechnicianId { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
    }
}