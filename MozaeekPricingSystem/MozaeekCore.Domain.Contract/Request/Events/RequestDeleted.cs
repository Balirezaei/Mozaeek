namespace MozaeekCore.Domain.Contract.Request.Events
{
    public class RequestDeleted
    {
        public long Id { get; set; }

        public RequestDeleted(long id)
        {
            Id = id;
        }
    }
}