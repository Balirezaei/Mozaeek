using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract
{
    public class TechnicianPersonalInfoDto
    {
        public long TechnicianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string IdentityNumber { get; set; }
    }
}
