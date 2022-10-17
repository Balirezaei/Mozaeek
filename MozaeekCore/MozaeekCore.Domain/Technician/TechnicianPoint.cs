namespace MozaeekCore.Domain
{
    public class TechnicianPoint
    {
        public long Id { get; set; }
        public Point Point { get; set; }
        public long PointId { get; set; }

        public Technician Technician { get; set; }
        public long TechnicianId { get; set; }
    }
}