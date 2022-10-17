using System;
using System.Collections.Generic;
using System.Text;
using MozaeekCore.Core.Events;

namespace MozaeekCore.Domain.Contract.IntegrationEvents
{
    public class ExecutiveTechnicianRegistered : IEvent
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        protected ExecutiveTechnicianRegistered()
        {
        }

        public ExecutiveTechnicianRegistered(long id, DateTime dateTime, string firstName, string lastName, string nationalCode)
        {
            Id = id;
            CreateDateTime = dateTime;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

    }
}
