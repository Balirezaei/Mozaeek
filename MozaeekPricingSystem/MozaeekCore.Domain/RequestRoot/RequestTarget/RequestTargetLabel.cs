
namespace MozaeekCore.Domain
{
    public class RequestTargetLabel
    {
        public long Id { get; set; }
        public long RequestTargetId { get; set; }
        public virtual RequestTarget RequestTarget { get; set; }

        public long LabelId { get; set; }
        public virtual Label Label { get; set; }

    }
}