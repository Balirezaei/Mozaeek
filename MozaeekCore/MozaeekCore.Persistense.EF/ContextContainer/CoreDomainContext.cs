using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain;
using MozaeekCore.Domain.Identity;
using MozaeekCore.Domain.Pricing;
using MozaeekCore.Enum;
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
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PreRequest> PreRequests { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<RequestAct> RequestActs { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<RequestTarget> RequestTargets { get; set; }
        public DbSet<RequestTargetLabel> RequestTargetLabels { get; set; }
        public DbSet<RequestTargetSubject> RequestTargetSubjects { get; set; }
        //public DbSet<RequestTargetRequestOrg> RequestTargetRequestOrgs { get; set; }

        public DbSet<AnnouncementLabel> AnnouncementLabels { get; set; }
        public DbSet<AnnouncementSubject> AnnouncementSubjects { get; set; }
        public DbSet<AnnouncementRequestOrg> AnnouncementRequestOrgs { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<RequestOrg> RequestOrgs { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RSS> Rsses { get; set; }
        public DbSet<RSSNews> RssNewses { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<MosaikFile> Files { get; set; }
        public DbSet<AnnouncementPoint> AnnouncementPoints { get; set; }
        public DbSet<RequestPriceHeader> RequestPriceHeaders { get; set; }
        public DbSet<RequestPriceDetail> RequestPriceDetails { get; set; }
        public DbSet<SubjectPriceHeader> SubjectPriceHeaders { get; set; }
        public DbSet<SubjectPriceDetail> SubjectPriceDetails { get; set; }

    }
}