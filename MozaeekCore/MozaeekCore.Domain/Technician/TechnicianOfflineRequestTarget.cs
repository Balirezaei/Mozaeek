namespace MozaeekCore.Domain
{
    public class TechnicianOfflineRequestTarget
    {
        public long Id { get; set; }
        public RequestTarget RquestTarget { get; set; }
        public long RquestTargetId { get; set; }

        public Technician Technician { get; set; }
        public long TechnicianId { get; set; }
    }
}