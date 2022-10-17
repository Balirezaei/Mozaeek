using MozaeekCore.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mozaeek.CR.PublicDto;
using MozaeekCore.Enum;
using MozaeekCore.Exceptions;

namespace MozaeekCore.Domain
{
    public class Technician : AggregateRootBase
    {
        public long Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string NationalId { get; set; }
        public string PostalCode { get; set; }
        public long? PointId { get; set; }
        public virtual Point Point { get; set; }
        public TechnicianType TechnicianType { get; set; }
        public virtual ICollection<TechnicianOfflineRequestTarget> TechnicianOflineRequestTargets { get; set; }
        public  virtual  ICollection<TechnicianDefiniteRequestOrg> TechnicianDefiniteRequestOrgs { get; set; }
        public  virtual ICollection<TechnicianSubject> TechnicianSubjects { get; set; }
        public  virtual ICollection<TechnicianAttachment> TechnicianAttachements { get; set; }
        public bool FirstVerification { get; set; }
        public bool SecondVerification { get; set; }
        public DateTime CreateTime { get; set; }
        public Technician(long id, string phoneNumber, string firstName, string lastName, string email, TechnicianType technicianType, string nationalId, string address,string postalCode, long? pointId = null, ICollection<TechnicianOfflineRequestTarget> technicianOflineRequestTargets = null, ICollection<TechnicianSubject> technicianSubjects = null, ICollection<TechnicianDefiniteRequestOrg> technicianDefiniteRequestOrgs = null, ICollection<TechnicianAttachment> technicianAttachements = null)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TechnicianType = technicianType;
            NationalId = nationalId;
            TechnicianSubjects = technicianSubjects;
            TechnicianOflineRequestTargets = technicianOflineRequestTargets;
            TechnicianDefiniteRequestOrgs = technicianDefiniteRequestOrgs;
            TechnicianAttachements = technicianAttachements;
            PostalCode = postalCode;
            PointId = pointId;
            CreateTime = DateTime.Now;

        }

        protected Technician(TechnicianType technicianType)
        {
            TechnicianType = technicianType;
        }

        public Technician(string phoneNumber, TechnicianType technicianType)
        {
            PhoneNumber = phoneNumber;
            TechnicianType = technicianType;
        }


        public string FullName()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
            {
                return $"{FirstName} {LastName}";
            }

            return "";
        }
    }
}


