using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain;
using MozaeekCore.ViewModel;

namespace MozaeekCore.Persistense.EF
{
    public class CoreDomainContext : DbContext
    {
        public CoreDomainContext()
        { }

        public CoreDomainContext(DbContextOptions<CoreDomainContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            //modelBuilder.Entity<RSSNews>(sd =>
            //{
            //    sd.HasNoKey().ToView(null);
            //});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UnProcessedRequest> UnProcessedRequests { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<RequestAct> RequestActs { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<RequestTarget> RequestTargets { get; set; }
        public DbSet<RequestTargetLabel> RequestTargetLabels { get; set; }
        public DbSet<RequestTargetSubject> RequestTargetSubjects { get; set; }
        public DbSet<RequestTargetRequestOrg> RequestTargetRequestOrgs { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<RequestOrg> RequestOrgs { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RSS> Rsses { get; set; }
        public DbSet<RSSNews> RssNewses { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementPoint> AnnouncementPoints { get; set; }

    }
}