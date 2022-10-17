using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract
{
    public class TechnicianDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
    }
}
