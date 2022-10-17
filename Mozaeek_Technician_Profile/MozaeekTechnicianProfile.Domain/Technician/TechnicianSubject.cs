namespace MozaeekTechnicianProfile.Domain
{
    public class TechnicianSubject
    {
        public long Id { get;  set; }
        /// <summary>
        /// Id come from CoreDomain
        /// </summary>
        public long SubjectId { get;  set; }
        public string SubjectTitle { get;  set; }
    }
}