using System;

namespace MozaeekUserProfile.Domain
{
    public class UserPoint
    {
        public UserPoint(long id, long pointId, long userId, string title)
        {
            Id = id;
            PointId = pointId;
            UserId = userId;
            Title = title;
            CreateDate = DateTime.Now;
        }

        public long Id { get; private set; }
        public long PointId { get; private set; }
        public long UserId { get; private set; }
        public string Title { get; set; }
        public virtual User User { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? EndDate { get; set; }
    }
}