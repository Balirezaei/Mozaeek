using Mozaeek.CR.PublicDto;
using System;
using System.Collections.Generic;
using System.Text;
using MozaeekTechnicianProfile.Common;

namespace MozaeekTechnicianProfile.ApplicationService.Contract.Dtos
{
    public class CreateTechnicianProfileDto
    {
        public long Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string NationalNumber { get; set; }
        public long? PointId { get; set; }
        public string LastRefreshToken { get; private set; }
        public TechnicianType TechnicianType { get; set; }
        public List<TechnicianOflineRequest> TechnicianOflineRequests { get; set; }
        public List<TechnicianDefiniteRequestOrg> TechnicianDefiniteRequestOrgs { get; set; }
        public List<TechnicianSubject> TechnicianSubjects { get; set; }
        public List<TechnicianAttachement> TechnicianAttachements { get; set; }
    }

    public class ActiveTechnicianInput
    {
        public long TechnicianId { get; set; }
    }

    public class TechnicianAbsencePresenceResult
    {
        public TechnicianAbsencePresenceState State { get; set; }
    }
    public class DeactiveTechnicianInput
    {
        public long TechnicianId { get; set; }
    }
}
