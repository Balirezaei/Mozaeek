using System;

namespace MozaeekCore.QueryModel
{
    public class DefiniteRequestOrgQueryParameter
    {
        public long Id { get; set; }
        public long RequestOrg { get;  set; }
        public long Point { get;  set; }
        public string Address { get;  set; }
        public string PhoneNumber { get;  set; }
        public DateTime LastEventPublishDate { get; set; }
        public Guid LastEventId { get; set; }
    }
}