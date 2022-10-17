using System.Collections.Generic;
using Mozaeek.CR.PublicDto;
using MozaeekTechnicianProfile.Common;

namespace MozaeekTechnicianProfile.Domain
{
    public class Technician
    {
        public long Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string NationalNumber { get; set; }
        public long? PointId { get; set; }
        public string IntroducerCode { get; set; }
        public virtual ICollection<TechnicianDiscount> UserDiscounts { get; set; }
        public string LastRefreshToken { get; private set; }
        public TechnicianType TechnicianType { get; set; }
        public TechnicianAbsencePresenceState LastAbsencePresenceState { get; private set; }
        public ICollection<TechnicianAbsencePresenceStateHistory> TechnicianAbsencePresenceStateHistories { get; private set; }
        public ICollection<TechnicianOflineRequest> TechnicianOflineRequests { get; set; }
        public ICollection<TechnicianDefiniteRequestOrg> TechnicianDefiniteRequestOrgs { get; set; }
        public ICollection<TechnicianSubject> TechnicianSubjects { get; set; }
        public ICollection<TechnicianAttachement> TechnicianAttachements { get; set; }

        public Technician(long id, string phoneNumber, string firstName, string lastName, string email, TechnicianType technicianType, string nationalNumber, string address, string lastRefereshToken, long? point = null, ICollection<TechnicianOflineRequest> technicianOflineRequests = null, ICollection<TechnicianSubject> technicianSubjects = null, ICollection<TechnicianDefiniteRequestOrg> technicianDefiniteRequestOrgs = null, ICollection<TechnicianAttachement> technicianAttachements = null)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            TechnicianType = technicianType;
            NationalNumber = nationalNumber;
            TechnicianSubjects = technicianSubjects;
            TechnicianOflineRequests = technicianOflineRequests;
            TechnicianDefiniteRequestOrgs = technicianDefiniteRequestOrgs;
            TechnicianAttachements = technicianAttachements;
            LastAbsencePresenceState = TechnicianAbsencePresenceState.ActiveReady;
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

        public void UpdateRefreshToken(string refreshToken)
        {
            this.LastRefreshToken = refreshToken;
        }
        public void UpdateTechnicianFirstName(string firstName)
        {
            this.FirstName = firstName;
        }
        public void UpdateTechnicianLastName(string lastName)
        {
            this.LastName = lastName;
        }
        public void UpdateTechnicianAddress(string address)
        {
            this.Address = address;
        }
        public void UpdateTechnicianEmail(string email)
        {
            this.Email = email;
        }
        public void UpdateTechnicianPhoneNumber(string pNmuber)
        {
            this.PhoneNumber = pNmuber;
        }
        public void UpdateTechnicianIntroducerCode(string introducerCode)
        {
            this.IntroducerCode = introducerCode;
        }
        public void UpdateTechnicianNationalId(string nationalId)
        {
            this.NationalNumber = nationalId;
        }
        public void ChangeTechnicianAbsencePresenceState(TechnicianAbsencePresenceState state)
        {
            if (this.LastAbsencePresenceState != state)
            {
                if (TechnicianAbsencePresenceStateHistories == null)
                    TechnicianAbsencePresenceStateHistories = new List<TechnicianAbsencePresenceStateHistory>();
                TechnicianAbsencePresenceStateHistories.Add(new TechnicianAbsencePresenceStateHistory(state, ""));
                LastAbsencePresenceState = state;
            }
        }
    }
}