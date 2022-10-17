namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestNecessityDto
    {
        public RequestNecessityDto(long id, string description, int priority)
        {
            Id = id;
            Description = description;
            Priority = priority;
        }

        public RequestNecessityDto()
        {
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}