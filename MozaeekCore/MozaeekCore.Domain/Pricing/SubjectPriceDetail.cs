namespace MozaeekCore.Domain.Pricing
{
    public class SubjectPriceDetail
    {
        protected SubjectPriceDetail() { }
        public long Id { get; private set; }
        public SubjectPriceHeader SubjectPriceHeader { get; private set; }
        public long SubjectPriceHeaderId { get; private set; }
        public Subject Subject { get; private set; }
        public long SubjectId { get; private set; }

        public SubjectPriceDetail(long subjectId)
        {
            SubjectId = subjectId;
        }

        public SubjectPriceDetail(long subjectPriceHeaderId, long subjectId)
        {
            SubjectPriceHeaderId = subjectPriceHeaderId;
            SubjectId = subjectId;
        }
    }
}