using Mozaeek.CR.PublicDto;
using MozaeekCore.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MozaeekCore.Domain.Contract.Technician
{

    public class TechnicianCreatedOrUpdated : IEvent
    {
        public TechnicianCreatedOrUpdated( long id, string phoneNumber, string firstName, string lastName, string email, string address, string nationalId, string postalCode, long? pointId, TechnicianType technicianType, List<long> offlineRequestTargetIds, List<long> definiteRequestOrgIds, List<long> subjectIds, List<TechnicianAttachment> attachments, bool firstVerification, bool secondVerification, DateTime createDateTime, bool isCreated)
        {
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
            Id = id;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            NationalId = nationalId;
            PostalCode = postalCode;
            PointId = pointId;
            TechnicianType = technicianType;
            OfflineRequestTargetIds = offlineRequestTargetIds;
            DefiniteRequestOrgIds = definiteRequestOrgIds;
            SubjectIds = subjectIds;
            Attachments = attachments;
            FirstVerification = firstVerification;
            SecondVerification = secondVerification;
            CreateDateTime = createDateTime;
            IsCreated = isCreated;
        }

        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NationalId { get; set; }
        public string PostalCode { get; set; }
        public long? PointId { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public List<long> OfflineRequestTargetIds { get; set; }
        public List<long> DefiniteRequestOrgIds { get; set; }
        public List<long> SubjectIds { get; set; }
        public List<TechnicianAttachment> Attachments { get; set; }
        public bool FirstVerification { get; set; }
        public bool SecondVerification { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsCreated { get; set; }
    }
    public class TechnicianAttachment
    {

        public long FileId { get; set; }
        public string FileName { get; set; }
        public string FileHttpAddress { get; set; }

    }

}