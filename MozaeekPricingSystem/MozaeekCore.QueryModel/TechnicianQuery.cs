using System;
using System.Collections.Generic;
using MozaeekCore.Enum;

namespace MozaeekCore.QueryModel
{
    public class TechnicianQuery
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public long EducationalFeildId { get; set; }
        public string EducationalFeildTitle { get; set; }
        public long EducationalGradeId { get; set; }
        public string EducationalGradeTitle { get; set; }
        public string ContactInfoMobileNumber { get; set; }
        public string ContactInfoPhoneNumber { get; set; }
        public string ContactInfoOfficeNumber { get; set; }
        public string ContactInfoPostalCode { get; set; }
        public string ContactInfoAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string IdentityNumber { get; set; }
        public List<PointQuery> Points { get; set; }
        public List<SubjectQuery> Subjects { get; set; }
        public List<RequestQuery> Requests { get; set; }

        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }
    }

    public class TechnicianPersonalInfoParameter
    {
        public long TechnicianId { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime PublishEventDate { get; set; }
        public Guid EventId { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class TechnicianContactInfoParameter
    {
        public long TechnicianId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishEventDate { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }

        public TechnicianContactInfoParameter(long technicianId, Guid eventId, DateTime publishEventDate, string mobileNumber, string phoneNumber, string officeNumber, string postalCode, string address)
        {
            TechnicianId = technicianId;
            EventId = eventId;
            PublishEventDate = publishEventDate;
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
            OfficeNumber = officeNumber;
            PostalCode = postalCode;
            Address = address;
        }
    }


    public class TechnicianEducationInfoParameter
    {
        public long TechnicianId { get; set; }
        public Guid EventId { get; set; }
        public DateTime PublishEventDate { get; set; }
        public long EducationGradeId { get; private set; }
        public string EducationGradeTitle { get; set; }

        public long EducationFieldId { get; private set; }
        public string EducationFieldTitle { get; set; }

        public TechnicianEducationInfoParameter(long technicianId, Guid eventId, DateTime publishEventDate, long educationGradeId, string educationGradeTitle, long educationFieldId, string educationFieldTitle)
        {
            TechnicianId = technicianId;
            EventId = eventId;
            PublishEventDate = publishEventDate;
            EducationGradeId = educationGradeId;
            EducationGradeTitle = educationGradeTitle;
            EducationFieldId = educationFieldId;
            EducationFieldTitle = educationFieldTitle;
        }
    }

    public class TechnicianRequestsParameter
    {
        public long TechnicianId { get; set; }
        public DateTime PublishEventDate { get; set; }
        public Guid EventId { get; set; }
        public List<long> Requests { get; set; }

        public TechnicianRequestsParameter(long technicianId, DateTime publishEventDate, Guid eventId, List<long> requests)
        {
            TechnicianId = technicianId;
            PublishEventDate = publishEventDate;
            EventId = eventId;
            Requests = requests;
        }
    }
    public class TechnicianSubjectsParameter
    {
        public long TechnicianId { get; set; }
        public DateTime PublishEventDate { get; set; }
        public Guid EventId { get; set; }
        public List<long> Subjects { get; set; }

        public TechnicianSubjectsParameter(long technicianId, DateTime publishEventDate, Guid eventId, List<long> requests)
        {
            TechnicianId = technicianId;
            PublishEventDate = publishEventDate;
            EventId = eventId;
            Subjects = requests;
        }
    }

    public class TechnicianPointsParameter
    {
        public long TechnicianId { get; set; }
        public DateTime PublishEventDate { get; set; }
        public Guid EventId { get; set; }
        public List<long> Points { get; set; }

        public TechnicianPointsParameter(long technicianId, DateTime publishEventDate, Guid eventId, List<long> requests)
        {
            TechnicianId = technicianId;
            PublishEventDate = publishEventDate;
            EventId = eventId;
            Points = requests;
        }
    }
}