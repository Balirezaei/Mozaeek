using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekTechnicianProfile.ApplicationService.Contract.Dtos
{
    public class UpdateTechnicinaContactInfoDto
    {
        public long TechnicianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string IntroducerCode { get; set; }
        public string NationalId { get; set; }
    }
}
