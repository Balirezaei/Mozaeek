namespace MozaeekCore.Domain
{
    public class TechnicianRequest
    {
        public long Id { get; set; }
        public Request Request { get; set; }
        public long RequestId { get; set; }

        public Technician Technician { get; set; }
        public long TechnicianId { get; set; }
    }
}