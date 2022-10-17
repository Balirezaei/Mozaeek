using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ViewModel
{
   public class TechnicianGuidViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNmuber { get; set; }
        public string NationalId { get; set; }
        public List<long> FileIds { get; set; }
        public List<long> SubjectIds { get; set; }
    }
}
