
namespace MozaeekCore.Domain
{
    public class RequestTargetRequestOrg
    {
        public long Id { get; set; }

        public long RequestTargetId { get; set; }
        public virtual RequestTarget RequestTarget { get; set; }

        public long RequestOrgId { get; set; }
        public virtual RequestOrg RequestOrg { get; set; }
    }
}