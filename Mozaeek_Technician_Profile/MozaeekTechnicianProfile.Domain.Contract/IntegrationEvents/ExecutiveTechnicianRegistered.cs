using System;
using System.Collections.Generic;
using System.Text;
using MozaeekTechnicianProfile.Core.Events;

namespace MozaeekTechnicianProfile.Domain.Contract.IntegrationEvents
{
    public class ExecutiveTechnicianRegistered : DomainEvent
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
    }
}
