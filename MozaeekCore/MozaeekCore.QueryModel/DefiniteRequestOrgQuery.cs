using System;

namespace MozaeekCore.QueryModel
{
    public class DefiniteRequestOrgQuery : BaseQuery
    {
        public long Id { get; set; }
        public RequestOrgQuery RequestOrg { get; private set; }
        public PointQuery Point { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }

        public string GetTitle()
        {
            return RequestOrg.Title + " - " + Point.Title;
        }
        public DefiniteRequestOrgQuery(long id, RequestOrgQuery requestOrg, PointQuery point, string address, string phoneNumber, DateTime lastEventPublishDate, Guid lastEventId)
        {
            Id = id;
            RequestOrg = requestOrg;
            Point = point;
            Address = address;
            PhoneNumber = phoneNumber;
            LastEventPublishDate = lastEventPublishDate;
            LastEventId = lastEventId;
        }

        public void Update(PointQuery point, string address, string phoneNumber, DateTime lastEventPublishDate, Guid lastEventId)
        {
            Point = point;
            Address = address;
            PhoneNumber = phoneNumber;
            LastEventPublishDate = lastEventPublishDate;
            LastEventId = lastEventId;
        }
    }
}