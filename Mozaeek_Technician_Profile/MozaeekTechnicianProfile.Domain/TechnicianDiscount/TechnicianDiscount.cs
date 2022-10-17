using System;

namespace MozaeekTechnicianProfile.Domain
{
    public class TechnicianDiscount
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Technician Technician { get; set; }
        public long TechnicianId { get; set; }
        public int DiscountAmount { get; set; }
        public short DiscountPercent { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
    }
}