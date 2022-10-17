using Microsoft.EntityFrameworkCore;
using MozaeekCore.TechnicianProfileConsistensyService.Model;

namespace MozaeekCore.TechnicianProfileConsistensyService.Context
{
    public class TechnicianProfileContext : DbContext
    {


        public TechnicianProfileContext(DbContextOptions<TechnicianProfileContext> options)
            : base(options)
        {

        }

        public DbSet<Tecnician> Tecnician { get; set; }
        public DbSet<UserQuestionWaitingForTechnician> UserQuestionWaitingForTechnicians { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<RssDto>(sd =>
            // {
            //     sd.HasNoKey().ToView(null);
            // });
            base.OnModelCreating(modelBuilder);
        }
    }
}