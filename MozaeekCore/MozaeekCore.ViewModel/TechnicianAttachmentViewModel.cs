using System.Collections.Generic;
using Mozaeek.CR.PublicDto;
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

    public class UpdateAnnouncementViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<long> Points { get; set; }
        public string Summary { get; set; }
        public List<long> RequestOrgs { get; set; }
        public List<long> Subjects { get; set; }
        public List<long> Labels { get; set; }
    }

}