namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestTargetConnectionDto
    {
        public RequestTargetConnectionDto(long requestTargetId, string requestTargetTitle, string description)
        {
            RequestTargetId = requestTargetId;
            RequestTargetTitle = requestTargetTitle;
            Description = description;
        }

        public RequestTargetConnectionDto()
        {
        }

        public long RequestTargetId { get; set; }
        public string RequestTargetTitle { get; set; }
        public string Description { get; set; }

    }
}