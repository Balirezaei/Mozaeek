using Microsoft.EntityFrameworkCore;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF
{
    public class MozaeekUserProfileContext : DbContext
    {

        public MozaeekUserProfileContext()
        {
        }

        public MozaeekUserProfileContext(DbContextOptions<MozaeekUserProfileContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("QuestionCode").IncrementsBy(1).StartsAt(100000);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        //entities
        public DbSet<User> Users { get; set; }
        public DbSet<OtpCode> OtpCodes { get; set; }
        public DbSet<UserDashboard> UserDashboards { get; set; }
        public DbSet<UserPoint> UserPoints { get; set; }
        public DbSet<UserQuestion> UserQuestions { get; set; }
        public DbSet<UserWallet> UserWallets { get; set; }
        public DbSet<UserProfileCharacteristicOwner> UserProfileCharacteristicOwners { get; set; }
        public DbSet<UserProfileCharacteristic> UserProfileCharacteristics { get; set; }
        public DbSet<UserDashboardCharacteristic> UserDashboardCharacteristics { get; set; }
    }
}