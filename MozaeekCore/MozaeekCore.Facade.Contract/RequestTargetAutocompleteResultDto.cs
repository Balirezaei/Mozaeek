namespace MozaeekCore.Facade.Contract
{
    public class RequestTargetAutocompleteResultDto
    {
        public RequestTargetAutocompleteResultDto(long id, string title)
        {
            Id = id;
            Title = title;
        }

        public long Id { get; set; }
        public string Title { get; set; }
    }
}