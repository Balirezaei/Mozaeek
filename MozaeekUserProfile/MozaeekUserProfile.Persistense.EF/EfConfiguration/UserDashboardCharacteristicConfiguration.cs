using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MozaeekUserProfile.Domain;

namespace MozaeekUserProfile.Persistense.EF.EfConfiguration
{
    public class UserDashboardCharacteristicConfiguration:IEntityTypeConfiguration<UserDashboardCharacteristic>
    {
        public void Configure(EntityTypeBuilder<UserDashboardCharacteristic> builder)
        {
            builder.ToTable("UserDashboardCharacteristic");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title).HasMaxLength(40);
            builder.HasOne(m => m.User)
                .WithMany(m => m.UserDashboardCharacteristics)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.UserProfileCharacteristic)
                .WithMany(m => m.UserDashboardCharacteristics)
                .HasForeignKey(m => m.UserProfileCharacteristicId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}