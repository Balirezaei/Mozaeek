using System;
using System.Collections.Generic;
using MozaeekCore.Core.Events;
using MozaeekCore.Enum;

namespace MozaeekCore.Domain.Contract
{
    public class TechnicianPersonalInfoCreatedOrUpdated : IEvent
    {
        public long TechnicianId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string IdentityNumber { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public bool IsCreated { get; set; }
        public DateTime CreateDate { get; set; }
        public TechnicianPersonalInfoCreatedOrUpdated(long technicianId, string firstName, string lastName, string nationalCode, string identityNumber, TechnicianType technicianType, DateTime createDate, bool isCreated)
        {
            TechnicianId = technicianId;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            IdentityNumber = identityNumber;
            TechnicianType = technicianType;
            IsCreated = isCreated;
            CreateDate = createDate;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        protected TechnicianPersonalInfoCreatedOrUpdated(bool isCreated, DateTime createDate)
        {
            IsCreated = isCreated;
            CreateDate = createDate;
        }
    }

    public class TechnicianContactInfoAdded : IEvent
    {
        public long TechnicianId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public string MobileNumber { get; private set; }
        public string PhoneNumber { get; private set; }
        public string OfficeNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string Address { get; private set; }

        public TechnicianContactInfoAdded(long technicianId, string mobileNumber, string phoneNumber, string officeNumber, string postalCode, string address)
        {
            TechnicianId = technicianId;
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
            OfficeNumber = officeNumber;
            PostalCode = postalCode;
            Address = address;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }

    public class TechnicianEducationInfoAdded : IEvent
    {
        public long TechnicianId { get; set; }
        public long EducationGradeId { get; private set; }
        public string EducationGradeTitle { get; set; }

        public long EducationFieldId { get; private set; }
        public string EducationFieldTitle { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }

        public TechnicianEducationInfoAdded(long technicianId, long educationGradeId, string educationGradeTitle, long educationFieldId, string educationFieldTitle)
        {
            TechnicianId = technicianId;
            EducationGradeId = educationGradeId;
            EducationGradeTitle = educationGradeTitle;
            EducationFieldId = educationFieldId;
            EducationFieldTitle = educationFieldTitle;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }

    public class TechnicianPointAdded : IEvent
    {
        public long TechnicianId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public List<long> Points { get; set; }

        public TechnicianPointAdded(long technicianId, List<long> points)
        {
            TechnicianId = technicianId;
            Points = points;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }
    public class TechnicianSubjectAdded : IEvent
    {
        public long TechnicianId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public List<long> Subjects { get; set; }
        public TechnicianSubjectAdded(long technicianId, List<long> subjects)
        {
            TechnicianId = technicianId;
            Subjects = subjects;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }

    public class TechnicianRequestAdded : IEvent
    {
        public long TechnicianId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishDateTime { get; set; }
        public List<long> Requests { get; set; }

        public TechnicianRequestAdded(long technicianId, List<long> requests)
        {
            TechnicianId = technicianId;
            Requests = requests;
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }
    }


}