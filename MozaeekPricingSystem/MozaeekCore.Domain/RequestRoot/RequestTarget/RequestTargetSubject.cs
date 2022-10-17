
namespace MozaeekCore.Domain
{
    public class RequestTargetSubject
    {
        public long Id { get; set; }
        public long RequestTargetId { get; set; }
        public virtual RequestTarget RequestTarget { get; set; }

        public long SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}