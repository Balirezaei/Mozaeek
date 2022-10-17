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
                  .Include(m => m.AnnouncementPoints)
                  .Include(m => m.AnnouncementLabels)
                  .Include(m => m.AnnouncementSubjects)
                  .Include(m => m.AnnouncementRequestOrgs)
                  .Include(m => m.File)
                  .FirstOrDefaultAsync();
        }

        public void ResetAssociations(Announcement announcement)
        {
            foreach (var label in announcement.AnnouncementPoints)
            {
                _context.AnnouncementPoints.Remove(label);
            }
            foreach (var label in announcement.AnnouncementLabels)
            {
                _context.AnnouncementLabels.Remove(label);
            }

            foreach (var subject in announcement.AnnouncementSubjects)
            {
                _context.AnnouncementSubjects.Remove(subject);
            }

            foreach (var requestOrg in announcement.AnnouncementRequestOrgs)
            {
                _context.AnnouncementRequestOrgs.Remove(requestOrg);
            }
        }

        public async Task CreatAnnouncement(Announcement announcement)
        {
            await _context.Announcements.AddAsync(announcement);
        }

        public void Delete(Announcement announcement)
        {
            _context.Announcements.Remove(announcement);
        }

        public async Task UpdateNewsRssState(long newsId)
        {
            var news = await _context.RssNewses.FindAsync(newsId);
            news.IsProcessed = true;
            _context.RssNewses.Update(news);
        }

        public void UpdateAnnouncement(Announcement announcement)
        {
            _context.Announcements.Update(announcement);
        }

    
    }
}