using Karmizban.Support.Domain;
using Microsoft.EntityFrameworkCore;

namespace Karmizban.Support.EF.ContextContainer
{
    public class SupportContext: DbContext
    {
        public SupportContext()
        { }

        public SupportContext(DbContextOptions<SupportContext> options)
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

        public DbSet<UserRequestSupport> UserRequestSupports { get; set; }
        public DbSet<UserSuggestedSupport> UserSuggestedSupports { get; set; }
        public DbSet<UserRequestSupportAnswer> UserRequestSupportAnswers { get; set; }
        public DbSet<TechnicianRequestSupport> TechnicianRequestSupports { get; set; }
        public DbSet<TechnicianRequestSupportAnswer> TechnicianRequestSupportAnswers { get; set; }
        public DbSet<TechnicianSuggestedSupport> TechnicianSuggestedSupports { get; set; }


    }
}
