namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class UpdateAnnouncementCommandResult
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? FileId { get; set; }
    }
}