using System;
using System.Collections.Generic;
using Mozaeek.CR.PublicDto;
using MozaeekCore.Enum;

namespace MozaeekCore.QueryModel
{
    public class TechnicianQuery : BaseQuery
    {
        public long Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string NationalId { get; set; }
        public string PostalCode { get; set; }
        public PointQuery Point { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public IList<RequestTargetQuery> OfflineRequestTargets { get; set; }
        public IList<DefiniteRequestOrgQuery> DefiniteRequestOrgs { get; set; }
        public IList<SubjectQuery> Subjects { get; set; }
        public IList<TechnicianAttachmentQuery> Attachments{ get; set; }
        public bool FirstVerification { get; set; }
        public bool SecondVerification { get; set; }
        public DateTime CreateDateTime { get; set; }

        public TechnicianQuery(long id, string phoneNumber, string firstName, string lastName, string email, string address, string nationalId, string postalCode, PointQuery point, TechnicianType technicianType, IList<RequestTargetQuery> offlineRequestTargets, IList<DefiniteRequestOrgQuery> definiteRequestOrgs, IList<SubjectQuery> subjects, IList<TechnicianAttachmentQuery> attachments, bool firstVerification, bool secondVerification, DateTime createDateTime)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            NationalId = nationalId;
            PostalCode = postalCode;
            Point = point;
            TechnicianType = technicianType;
            OfflineRequestTargets = offlineRequestTargets;
            DefiniteRequestOrgs = definiteRequestOrgs;
            Subjects = subjects;
            Attachments = attachments;
            FirstVerification = firstVerification;
            SecondVerification = secondVerification;
            CreateDateTime = createDateTime;
            LastEventPublishDate = DateTime.Now;
            LastEventId = Guid.NewGuid();
        }
    }

    public class TechnicianParameter
    {
        public long Id { get;  set; }
        public string PhoneNumber { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Email { get;  set; }
        public string Address { get;  set; }
        public string NationalId { get; set; }
        public string PostalCode { get; set; }
        public long? PointId { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public List<long> OfflineRequestTargetIds { get; set; }
        public List<long> DefiniteRequestOrgIds { get; set; }
        public List<long> SubjectIds { get; set; }
        public List<TechnicianAttachmentQuery> Attachments { get; set; }
        public bool FirstVerification { get; set; }
        public bool SecondVerification { get; set; }
        public DateTime CreateDateTime { get; set; }
    }


    //public class TechnicianPersonalInfoParameter
    //{
    //    public long TechnicianId { get; set; }
    //    public TechnicianType TechnicianType { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string NationalCode { get; set; }
    //    public string IdentityNumber { get; set; }
    //    public DateTime PublishEventDate { get; set; }
    //    public Guid EventId { get; set; }
    //    public DateTime CreateDate { get; set; }
    //}

    //public class TechnicianContactInfoParameter
    //{
    //    public long TechnicianId { get; set; }
    //    public Guid EventId { get; set; }
    //    public DateTime PublishEventDate { get; set; }
    //    public string MobileNumber { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string OfficeNumber { get; set; }
    //    public string PostalCode { get; set; }
    //    public string Address { get; set; }

    //    public TechnicianContactInfoParameter(long technicianId, Guid eventId, DateTime publishEventDate, string mobileNumber, string phoneNumber, string officeNumber, string postalCode, string address)
    //    {
    //        TechnicianId = technicianId;
    //        EventId = eventId;
    //        PublishEventDate = publishEventDate;
    //        MobileNumber = mobileNumber;
    //        PhoneNumber = phoneNumber;
    //        OfficeNumber = officeNumber;
    //        PostalCode = postalCode;
    //        Address = address;
    //    }
    //}


    //public class TechnicianEducationInfoParameter
    //{
    //    public long TechnicianId { get; set; }
    //    public Guid EventId { get; set; }
    //    public DateTime PublishEventDate { get; set; }
    //    public long EducationGradeId { get; private set; }
    //    public string EducationGradeTitle { get; set; }

    //    public long EducationFieldId { get; private set; }
    //    public string EducationFieldTitle { get; set; }

    //    public TechnicianEducationInfoParameter(long technicianId, Guid eventId, DateTime publishEventDate, long educationGradeId, string educationGradeTitle, long educationFieldId, string educationFieldTitle)
    //    {
    //        TechnicianId = technicianId;
    //        EventId = eventId;
    //        PublishEventDate = publishEventDate;
    //        EducationGradeId = educationGradeId;
    //        EducationGradeTitle = educationGradeTitle;
    //        EducationFieldId = educationFieldId;
    //        EducationFieldTitle = educationFieldTitle;
    //    }
    //}

    //public class TechnicianRequestsParameter
    //{
    //    public long TechnicianId { get; set; }
    //    public DateTime PublishEventDate { get; set; }
    //    public Guid EventId { get; set; }
    //    public List<long> Requests { get; set; }

    //    public TechnicianRequestsParameter(long technicianId, DateTime publishEventDate, Guid eventId, List<long> requests)
    //    {
    //        TechnicianId = technicianId;
    //        PublishEventDate = publishEventDate;
    //        EventId = eventId;
    //        Requests = requests;
    //    }
    //}
    //public class TechnicianSubjectsParameter
    //{
    //    public long TechnicianId { get; set; }
    //    public DateTime PublishEventDate { get; set; }
    //    public Guid EventId { get; set; }
    //    public List<long> Subjects { get; set; }

    //    public TechnicianSubjectsParameter(long technicianId, DateTime publishEventDate, Guid eventId, List<long> requests)
    //    {
    //        TechnicianId = technicianId;
    //        PublishEventDate = publishEventDate;
    //        EventId = eventId;
    //        Subjects = requests;
    //    }
    //}

    //public class TechnicianPointsParameter
    //{
    //    public long TechnicianId { get; set; }
    //    public DateTime PublishEventDate { get; set; }
    //    public Guid EventId { get; set; }
    //    public List<long> Points { get; set; }

    //    public TechnicianPointsParameter(long technicianId, DateTime publishEventDate, Guid eventId, List<long> requests)
    //    {
    //        TechnicianId = technicianId;
    //        PublishEventDate = publishEventDate;
    //        EventId = eventId;
    //        Points = requests;
    //    }
    //}

}