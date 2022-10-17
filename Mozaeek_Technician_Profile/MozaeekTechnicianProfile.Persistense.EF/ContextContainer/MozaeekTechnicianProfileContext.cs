using Microsoft.EntityFrameworkCore;
using MozaeekTechnicianProfile.Domain;

namespace MozaeekTechnicianProfile.Persistense.EF
{
    public class MozaeekTechnicianProfileContext : DbContext
    {
        public MozaeekTechnicianProfileContext()
        { }

        public MozaeekTechnicianProfileContext(DbContextOptions<MozaeekTechnicianProfileContext> options)
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
        
        //entities
        public DbSet<Technician> Users { get; set; }
        public DbSet<OtpCode> OtpCodes { get; set; }
        public DbSet<UserQuestionWaitingForTechnician> UserQuestionWaitingForTechnicians { get; set; }
        public DbSet<Technician> Technicians { get; set; }
    }
}