using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> Find(long id);
        void ResetAssociations(Announcement announcement);
        void UpdateAnnouncement(Announcement announcement);
        Task CreatAnnouncement(Announcement announcement);
        void Delete(Announcement announcement);
        Task UpdateNewsRssState(long newsId);
    }
}