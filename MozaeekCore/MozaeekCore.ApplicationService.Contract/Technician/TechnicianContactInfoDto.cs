using System.Collections.Generic;

namespace MozaeekCore.ApplicationService.Contract
{
    public class TechnicianContactInfoDto
    {
        public long TechnicianId { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }

    public class TechnicianRequestDto
    {
        public long TechnicianId { get; set; }
        public List<long> Requests { get; set; }
    }

    public class TechnicianPointDto
    {
        public long TechnicianId { get; set; }
        public List<long> Points { get; set; }

    }

    public class TechnicianSubjectDto
    {
        public long TechnicianId { get; set; }
        public List<long> Subjects { get; set; }
    }
}