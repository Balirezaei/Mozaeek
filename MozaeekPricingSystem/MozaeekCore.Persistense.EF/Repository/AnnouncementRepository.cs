using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain;

namespace MozaeekCore.Persistense.EF.Repository
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly CoreDomainContext _context;

        public AnnouncementRepository(CoreDomainContext context)
        {
            _context = context;
        }
        public Task<Announcement> Find(long id)
        {
            return _context.Announcements.Where(m => m.Id == id)
                  .Include(m => m.RequestTarget)
                  .Include(m => m.AnnouncementPoints).FirstOrDefaultAsync();
        }

        public void ResetAssociations(Announcement announcement)
        {
            foreach (var label in announcement.AnnouncementPoints)
            {
                _context.AnnouncementPoints.Remove(label);
            }
        }

        public async Task CreatAnnouncement(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
        }

        public void UpdateNewsRssState(long newsId)
        {
            var news = _context.RssNewses.Find(newsId);
            news.IsProcessed = true;
            _context.RssNewses.Update(news);
        }

        public void Delete(Announcement announcement)
        {
            _context.Announcements.Remove(announcement);
        }

        public void UpdateAnnouncement(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
        }
    }
}