using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.TechnicianProfileConsistensyService.Model
{
    public class Tecnician
    {
        public Tecnician(long technicianId, string firstName, string lastName, string mobileNumber, string nationalCode, bool isConsultant)
        {
            TechnicianId = technicianId;
            FirstName = firstName;
            LastName = lastName;
            MobileNumber = mobileNumber;
            NationalCode = nationalCode;
            IsConsultant = isConsultant;
        }

        public long Id { get; set; }
        public long TechnicianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public bool IsConsultant { get; set; }
    }
}
