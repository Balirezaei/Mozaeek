using Microsoft.AspNetCore.Http;
using Mozaeek.CR.PublicDto.Enum.CoreDomain;
using MozaeekCore.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.ApplicationService.Contract.Technician
{
    public class CreateAgentTechnicianCommand: Command
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
