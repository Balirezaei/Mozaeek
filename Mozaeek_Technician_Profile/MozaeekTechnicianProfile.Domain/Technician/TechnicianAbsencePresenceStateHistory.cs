using System;
using MozaeekTechnicianProfile.Common;

namespace MozaeekTechnicianProfile.Domain
{
    public class TechnicianAbsencePresenceStateHistory
    {
        protected TechnicianAbsencePresenceStateHistory()
        {
        }
        public TechnicianAbsencePresenceStateHistory( TechnicianAbsencePresenceState technicianAbsencePresenceState, string description)
        {
            CreateDate = DateTime.Now;
            TechnicianAbsencePresenceState = technicianAbsencePresenceState;
            Description = description;
        }

        public long Id { get; private set; }
        public DateTime CreateDate { get; private set; }
        public TechnicianAbsencePresenceState TechnicianAbsencePresenceState { get; private set; }
        public string Description { get; private set; }
    }
}