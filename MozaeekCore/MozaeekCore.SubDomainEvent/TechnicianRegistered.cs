using System;
using MozaeekCore.Core.Events;

namespace MozaeekCore.SubDomainEvent
{
    public class TechnicianRegistered : IEvent
    {
        public long TechnicianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string MobileNumber { get; set; }
        public bool IsConsultant { get; set; }
        public DateTime PublishDateTime { get; set; }
        public Guid EventId { get; set; }

        public TechnicianRegistered(long technicianId, string firstName, string lastName, string nationalCode, string mobileNumber, bool isConsultant)
        {
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            MobileNumber = mobileNumber;
            IsConsultant = isConsultant;
            TechnicianId = technicianId;
            PublishDateTime = DateTime.Now;
            EventId = Guid.NewGuid();
        }
    }
}