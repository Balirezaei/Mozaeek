namespace MozaeekCore.ApplicationService.Contract
{
    public class PreRequestDto
    {
        public long Id { get;  set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool IsProcessed { get; set; }
    }
}