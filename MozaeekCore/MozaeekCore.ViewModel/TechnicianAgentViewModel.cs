using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;

namespace MozaeekCore.ViewModel
{
    public class TechnicianAgentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNmuber { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public long PointId { get; set; }
        public List<long> FileIds { get; set; }
    }

}
