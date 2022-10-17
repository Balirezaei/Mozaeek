using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MozaeekCore.RSSRetrive.Model;
using MozaeekCore.ViewModel;

namespace MozaeekCore.RSSRetrive.Context
{
    public class FeedContext: DbContext
    {
        
        public FeedContext(DbContextOptions<FeedContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<RssDto> RssDtos { get; set; }
        public DbSet<RSSNews> RssNewses { get; set; }
        public DbSet<RssRetriveHistory> RssRetriveHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RssDto>(sd =>
            {
                sd.HasNoKey().ToView(null);
            });
            base.OnModelCreating(modelBuilder);
        }
    }

    public class FeedContextFactory : IDesignTimeDbContextFactory<FeedContext>
    {
        public FeedContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FeedContext>();

            optionsBuilder.UseSqlServer("integrated Security=True;Initial Catalog=CoreDomain1;Data Source=.");

            return new FeedContext(optionsBuilder.Options);
        }
    }
}