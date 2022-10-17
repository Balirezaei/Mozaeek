using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserDashboardConfiguration: IEntityTypeConfiguration<UserDashboard>
    {
        public void Configure(EntityTypeBuilder<UserDashboard> builder)
        {

            builder.ToTable("UserDashboard");
            builder.Property(m => m.Title).HasMaxLength(50);
            builder
                .HasOne(m => m.User)
                .WithMany(m => m.UserDashboards)
                .HasForeignKey(m => m.UserId);
        }
    }
}