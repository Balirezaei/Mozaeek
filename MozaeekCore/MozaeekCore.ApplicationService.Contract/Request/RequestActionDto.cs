namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestActionDto
    {
        public RequestActionDto(long id, string description, int priority)
        {
            Id = id;
            Description = description;
            Priority = priority;
        }

        public RequestActionDto()
        {
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}