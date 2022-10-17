namespace MozaeekCore.Domain
{
    public class TechnicianSubject
    {
        public long Id { get; set; }
        public Subject Subject { get; set; }
        public long SubjectId { get; set; }

        public Technician Technician { get; set; }
        public long TechnicianId { get; set; }
    }
}