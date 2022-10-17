namespace Mozaeek.Notification.Domain.Dtos
{
    public abstract class MessageDto
    {
        public string Message { get; set; }
        public string CorrelationId { get; set; }
    }
}
