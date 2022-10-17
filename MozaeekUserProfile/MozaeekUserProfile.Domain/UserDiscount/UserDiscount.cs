using System;

namespace MozaeekUserProfile.Domain
{
    public class UserDiscount
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public int DiscountAmount { get; set; }
        public short DiscountPercent { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsActive { get; set; }
    }
}