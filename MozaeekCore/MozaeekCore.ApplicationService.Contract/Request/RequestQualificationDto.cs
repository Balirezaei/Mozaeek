namespace MozaeekCore.ApplicationService.Contract
{
    public class RequestQualificationDto
    {
        public RequestQualificationDto(long id, string description, int priority)
        {
            Id = id;
            Description = description;
            Priority = priority;
        }

        public RequestQualificationDto()
        {
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}