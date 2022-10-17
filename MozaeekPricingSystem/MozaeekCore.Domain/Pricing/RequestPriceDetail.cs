namespace MozaeekCore.Domain.Pricing
{
    public class RequestPriceDetail
    {
        public long Id { get; private set; }
        public RequestPriceHeader RequestPriceHeader { get; private set; }
        public long RequestPriceHeaderId { get; private set; }
        public Request Request { get; private set; }
        public long RequestId { get; private set; }

        public RequestPriceDetail(long requestId)
        {
            RequestId = requestId;
        }

        public RequestPriceDetail(long requestPriceHeaderId, long requestId)
        {
            RequestPriceHeaderId = requestPriceHeaderId;
            RequestId = requestId;
        }
    }
}