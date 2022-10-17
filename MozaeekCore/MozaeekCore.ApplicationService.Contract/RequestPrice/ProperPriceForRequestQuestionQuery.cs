namespace MozaeekCore.ApplicationService.Contract
{
    public class ProperPriceForRequestQuestionQuery
    {
        public long RequestId { get; set; }

        public ProperPriceForRequestQuestionQuery(long requestId)
        {
            RequestId = requestId;
        }
    }
}