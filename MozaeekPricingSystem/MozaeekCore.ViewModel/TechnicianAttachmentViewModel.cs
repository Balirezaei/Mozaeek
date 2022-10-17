using MozaeekCore.Enum;

namespace MozaeekCore.ViewModel
{
    public class TechnicianAttachmentViewModel
    {
        public long TechnicianId { get; set; }
        public AttachmentType AttachmentType { get; set; }
    }

    public class CreateTechnicianPersonalInfoViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string IdentityNumber { get; set; }
        public TechnicianType TechnicianType { get; set; }
    }

    public class UpdateTechnicianPersonalInfoViewModel
    {
        public long TechnicianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string IdentityNumber { get; set; }
        public TechnicianType TechnicianType { get; set; }
    }
}