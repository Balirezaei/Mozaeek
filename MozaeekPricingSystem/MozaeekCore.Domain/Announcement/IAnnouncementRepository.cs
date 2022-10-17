using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> Find(long id);
        void ResetAssociations(Announcement announcement);
        void UpdateAnnouncement(Announcement announcement);
        Task CreatAnnouncement(Announcement announcement);
        void UpdateNewsRssState(long newsId);
        void Delete(Announcement announcement);
    }
}