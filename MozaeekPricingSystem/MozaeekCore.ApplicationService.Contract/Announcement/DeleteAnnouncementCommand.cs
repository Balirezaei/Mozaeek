using MozaeekCore.Core.Base;

namespace MozaeekCore.ApplicationService.Contract.Announcement
{
    public class DeleteAnnouncementCommand : Command
    {
        public DeleteAnnouncementCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}